using AutoMapper;
using Server.Core.Common.Messages.Identifiable;
using Server.Core.Common.Workflow;
using Server.Core.Common.Workflow.CheckListExists;
using Server.Core.Common.Workflow.CheckUserExists;
using Server.Core.Common.Workflow.CheckUserListExists;
using Server.Core.Lists.Messages.Identifiable;
using Server.Core.Lists.Workflow.AddNewList;
using Server.Core.Lists.Workflow.AddNewListItem;
using Server.Core.Lists.Workflow.CheckListItemExists;
using Server.Core.Lists.Workflow.GetUserFeed;
using Server.Core.Lists.Workflow.GetUserLists;
using Server.Core.Lists.Workflow.MoveToListItem;
using Server.Core.Lists.Workflow.RemoveList;
using Server.Core.Lists.Workflow.RemoveListItem;
using Server.Core.Lists.Workflow.UpdateList;
using Server.Core.Lists.Workflow.UpdateListCheckItemKind;
using Server.Core.Lists.Workflow.UpdateListItem;
using Server.Core.Lists.Workflow.UpdateListItemCheck;
using Server.Core.Lists.Workflow.UpdateListKind;
using Server.Core.Lists.Workflow.UpdatePublished;

namespace Server.Core.Lists.Workflow
{
    /// <summary>
    /// Конфигуратор для параметровпроцеса списков.
    /// </summary>
    class ListParametersBinder: StepParametersBinder
    {
        /// <summary>
        /// Должен переопределяться для правил биндинга параметров.
        /// </summary>
        /// <param name="mapperProfile">Профиль для регистрации.</param>
        public override void Configure(Profile mapperProfile)
        {
            mapperProfile.CreateMap<GetUserListsRequest, CheckUserExistsParams>()
                .ForMember(m => m.InputMessage, i => i.MapFrom(m => m));
            mapperProfile.CreateMap<CheckUserExistsParams, FetchUserListsParams>()
                .ForMember(m => m.LastListId, i => i.MapFrom(l => ((GetUserListsRequest)l.InputMessage ).LastListId))
                .ForMember(m => m.SelectedListNumber, i => i.MapFrom(l => ((GetUserListsRequest)l.InputMessage ).SelectedListNumber))
                .ForMember(m => m.ForUserName, i => i.MapFrom(l => ((GetUserListsRequest)l.InputMessage ).ForUserName))
                .ForMember(m => m.ForUserId, i => i.MapFrom(l => ((GetUserListsRequest)l.InputMessage ).ForUserId));
            
            mapperProfile.CreateMap<CheckUserExistsParams, CheckListExistsParams>()
                .ForMember(m=>m.ListId,l=>l.MapFrom(i=>((IListIdentifiable)i.InputMessage).ListId));
            mapperProfile.CreateMap<CheckListExistsParams, ListNotFoundParams>();
            

            mapperProfile.CreateMap<AddNewListRequest, CheckUserExistsParams>()
                .ForMember(m => m.InputMessage, i => i.MapFrom(m => m));
            mapperProfile.CreateMap<CheckUserExistsParams, AddNewListParams>().
                ForMember(m => m.Name, i => i.MapFrom(l => ((AddNewListRequest)l.InputMessage).Name));


            mapperProfile.CreateMap<UpdateListRequest, CheckUserExistsParams>().
                ForMember(m=>m.InputMessage,i=>i.MapFrom(l=>l));
            mapperProfile.CreateMap<CheckListExistsParams, UpdateListParams>().
                ForMember(m => m.Name, i => i.MapFrom(l => ((UpdateListRequest)l.InputMessage).Name)).
                ForMember(m => m.ListId, i => i.MapFrom(l => ((UpdateListRequest)l.InputMessage).ListId));

            BindRemoveList(mapperProfile);
            BindAddNewListItem(mapperProfile);
            BindCheckListItemExists(mapperProfile);
            BindUpdateListItem(mapperProfile);
            BindRemoveListItem(mapperProfile);
            BindMoveToListItem(mapperProfile);
            BindGetUserFeed(mapperProfile);
            BindUpdateListKind(mapperProfile);
            BindCheckUserListExists(mapperProfile);
            BindUpdateListCheckItemKind(mapperProfile);
            BindUpdateListItemCheck(mapperProfile);
            BindUpdatePublished(mapperProfile);
        }

        private static void BindUpdateListItemCheck(Profile mapperProfile)
        {
            mapperProfile.CreateMap<UpdateListItemCheckRequest, CheckUserExistsParams>().
                ForMember(m => m.InputMessage, i => i.MapFrom(l => l));

            mapperProfile.CreateMap<CheckListItemExistsParams, UpdateListItemCheckParams>().
                ForMember(m => m.ListId, i => i.MapFrom(l => ((IListIdentifiable)l.InputMessage).ListId)).
                ForMember(m => m.ListItemId, i => i.MapFrom(l => ((IListItemIdentifiable)l.InputMessage).ListItemId))
                .ForMember(m => m.IsChecked, i => i.MapFrom(l => ((UpdateListItemCheckRequest)l.InputMessage).IsChecked));
        }

        private static void BindCheckUserListExists(Profile mapperProfile)
        {
            mapperProfile.CreateMap<CheckUserExistsParams, CheckUserListExistsParams>()
                .ForMember(m => m.ListId, l => l.MapFrom(i => ((IListIdentifiable)i.InputMessage).ListId));
            mapperProfile.CreateMap<CheckUserListExistsParams, UserListNotFoundParams>();
        }

        private static void BindUpdateListCheckItemKind(Profile mapperProfile)
        {
            mapperProfile.CreateMap<UpdateListCheckItemKindRequest, CheckUserExistsParams>().
                ForMember(m => m.InputMessage, i => i.MapFrom(l => l));

            mapperProfile.CreateMap<CheckUserListExistsParams, UpdateListCheckItemKindParams>().
                ForMember(m => m.ListId, i => i.MapFrom(l => ((IListIdentifiable)l.InputMessage).ListId))
                .ForMember(m => m.ListCheckItemKindId, i => i.MapFrom(l => ((UpdateListCheckItemKindRequest)l.InputMessage).ListCheckItemKind));
        }

        private static void BindUpdateListKind(Profile mapperProfile)
        {
            mapperProfile.CreateMap<UpdateListKindRequest, CheckUserExistsParams>().
                ForMember(m => m.InputMessage, i => i.MapFrom(l => l));

            mapperProfile.CreateMap<CheckUserListExistsParams, UpdateListKindParams>().
                ForMember(m => m.ListId, i => i.MapFrom(l => ((IListIdentifiable)l.InputMessage).ListId))
                .ForMember(m => m.ListKindId, i => i.MapFrom(l => ((UpdateListKindRequest)l.InputMessage).ListKind));
        }

        private void BindGetUserFeed(Profile mapperProfile)
        {
            mapperProfile.CreateMap<GetUserFeedRequest, CheckUserExistsParams>().
                ForMember(m => m.InputMessage, i => i.MapFrom(l => l));

            mapperProfile.CreateMap<CheckUserExistsParams, GetUserFeedParams>().
                ForMember(m => m.LastListId, i => i.MapFrom(l => ((GetUserFeedRequest)l.InputMessage).LastListId));
        }


        private static void BindRemoveList(Profile mapperProfile)
        {
            mapperProfile.CreateMap<RemoveListRequest, CheckUserExistsParams>().
                ForMember(m => m.InputMessage, i => i.MapFrom(l => l));

            mapperProfile.CreateMap<CheckListExistsParams, RemoveListParams>().
                ForMember(m => m.ListId, i => i.MapFrom(l => ((IListIdentifiable)l.InputMessage).ListId));
        }

        private static void BindAddNewListItem(Profile mapperProfile)
        {
            mapperProfile.CreateMap<AddNewListItemRequest, CheckUserExistsParams>().
                ForMember(m => m.InputMessage, i => i.MapFrom(l => l));

            mapperProfile.CreateMap<CheckListExistsParams, AddNewListItemParams>().
                ForMember(m => m.ListId, i => i.MapFrom(l => ((IListIdentifiable)l.InputMessage).ListId))
                .ForMember(m => m.Content, i => i.MapFrom(l => ((AddNewListItemRequest)l.InputMessage).Content))
                .ForMember(m => m.AfterListItemId, i => i.MapFrom(l => ((AddNewListItemRequest)l.InputMessage).AfterListItemId));
        }

        private static void BindCheckListItemExists(Profile mapperProfile)
        {
            mapperProfile.CreateMap<CheckListExistsParams, CheckListItemExistsParams>()
                .ForMember(m=>m.ListItemId,l=>l.MapFrom(p=>((IListItemIdentifiable)p.InputMessage).ListItemId));
            mapperProfile.CreateMap<CheckUserListExistsParams, CheckListItemExistsParams>()
                .ForMember(m => m.ListItemId, l => l.MapFrom(p => ((IListItemIdentifiable)p.InputMessage).ListItemId));
            mapperProfile.CreateMap<CheckListItemExistsParams, ListItemNotFoundParams>();
        }

        private static void BindRemoveListItem(Profile mapperProfile)
        {
            mapperProfile.CreateMap<RemoveListItemRequest, CheckUserExistsParams>().
                ForMember(m => m.InputMessage, i => i.MapFrom(l => l));

            mapperProfile.CreateMap<CheckListItemExistsParams, RemoveListItemParams>().
                ForMember(m => m.ListItemId, i => i.MapFrom(l => ((IListItemIdentifiable)l.InputMessage).ListItemId));
        }
        
        private static void BindUpdateListItem(Profile mapperProfile)
        {
            mapperProfile.CreateMap<UpdateListItemRequest, CheckUserExistsParams>().
                ForMember(m => m.InputMessage, i => i.MapFrom(l => l));

            mapperProfile.CreateMap<CheckListItemExistsParams, UpdateListItemParams>().
                ForMember(m => m.ListId, i => i.MapFrom(l => ((IListIdentifiable)l.InputMessage).ListId)).
                ForMember(m => m.ListItemId, i => i.MapFrom(l => ((IListItemIdentifiable)l.InputMessage).ListItemId))
                .ForMember(m => m.Content, i => i.MapFrom(l => ((UpdateListItemRequest)l.InputMessage).Content));
        }

        private static void BindMoveToListItem(Profile mapperProfile)
        {
            mapperProfile.CreateMap<MoveToListItemRequest, CheckUserExistsParams>().
                ForMember(m => m.InputMessage, i => i.MapFrom(l => l));

            mapperProfile.CreateMap<CheckUserExistsParams, MoveToListItemParams>()
                .ForMember(m => m.AfterListItemId,
                    i => i.MapFrom(l => ((MoveToListItemRequest) l.InputMessage).AfterListItemId))
                .ForMember(m => m.Copy, i => i.MapFrom(l => ((MoveToListItemRequest) l.InputMessage).Copy))
                .ForMember(m => m.TargetListId, i => i.MapFrom(l => ((MoveToListItemRequest) l.InputMessage).TargetListId))
                .ForMember(m => m.ListItemId, i => i.MapFrom(l => ((MoveToListItemRequest) l.InputMessage).ListItemId));
                

            mapperProfile.CreateMap<MoveToListItemParams, ListNotFoundParams>()
                .ForMember(m => m.ListId, l => l.MapFrom(p => p.IdNotFound));
            mapperProfile.CreateMap<MoveToListItemParams, ListItemNotFoundParams>()
                .ForMember(m => m.ListItemId, l => l.MapFrom(p => p.IdNotFound));
        }

        private static void BindUpdatePublished(Profile mapperProfile)
        {
            mapperProfile.CreateMap<UpdatePublishedRequest, CheckUserExistsParams>().
                ForMember(m => m.InputMessage, i => i.MapFrom(l => l));

            mapperProfile.CreateMap<CheckUserListExistsParams, UpdatePublishedParams>().
                ForMember(m => m.ListId, i => i.MapFrom(l => ((IListIdentifiable)l.InputMessage).ListId))
                .ForMember(m => m.IsPublished, i => i.MapFrom(l => ((UpdatePublishedRequest)l.InputMessage).IsPublished));
        }
    }
}
