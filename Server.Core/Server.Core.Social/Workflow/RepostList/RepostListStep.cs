using System;
using System.Threading.Tasks;
using Server.Core.Common;
using Server.Core.Common.Entities.Lists;
using Server.Core.Common.Repositories.Lists;
using Server.Core.Common.Repositories.Users;
using Server.Core.Common.Workflow;

namespace Server.Core.Social.Workflow.RepostList
{
    /// <summary>
    /// Шаг по репосту списка.
    /// </summary>
    public class RepostListStep:StepBase<RepostListParams>
    {
        /// <summary>
        /// Реализация исполнения шага.
        /// </summary>
        /// <param name="state">Параметры шага.</param>
        /// <returns>Результат действия.</returns>
        public override async Task<StepResult> Execute(RepostListParams state)
        {
            var listRepository = StartEnumServer.Instance.GetRepository<IListRepository>();
            var listItemRepository = StartEnumServer.Instance.GetRepository<IListItemRepository>();
            var userRepository = StartEnumServer.Instance.GetRepository<IPortalUserRepository>();

            listRepository.ShareContext(listItemRepository);
            var newList = new List();

            using (var trans = listRepository.GetTransaction())
            {
                trans.Begin();

                var list = await listRepository.GetById(state.ListId);

                var user = await userRepository.GetById(list.PortalUserID??Guid.Empty);

                newList.PortalUserID = state.User.PortalUserID;
                newList.CreateEventTimeUTC = DateTime.UtcNow;
                newList.ListID = Guid.NewGuid();
                newList.Name = list.Name;
                newList.OriginalListID = list.ListID;
                newList.OriginalPortalUserID = list.PortalUserID;
                if (user!=null)
                {
                    newList.OriginFirstName = user.FirstName;
                    newList.OriginLastName = user.LastName;
                    newList.OriginMiddleName = user.MiddleName;
                    newList.OriginUserName = user.UserName;
                }

                await listRepository.SaveNew(newList);
                var items = listItemRepository.GetItemsByListId(list.ListID);

                foreach (var listItem in items)
                {
                    var newItem = new ListItem();
                    listItem.CopyTo(newItem);
                    newItem.ListID = newList.ListID;
                    newItem.PortalUserID = state.User.PortalUserID;
                    newItem.IsChecked = false;
                    newItem.LikeCount = default(int);
                    newItem.ListItemID= Guid.NewGuid();

                    await listItemRepository.SaveNew(newItem);
                }


                await listRepository.IncrementRepostCount(list.ListID);

                trans.Commit();
            }


            state.Response = new RepostListResponse();
            state.Response.ListId = state.ListId;
            state.Response.RepostedListId = newList.ListID;
            state.Response.RepostCount = await listRepository.GetRepostCount(state.ListId);

            return Success();
        }
    }
}
