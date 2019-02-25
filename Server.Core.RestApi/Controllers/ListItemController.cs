using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Core.Common.Messages;
using Server.Core.Common.Workflow;
using Server.Core.Common.Workflow.CheckListExists;
using Server.Core.Common.Workflow.CheckUserExists;
using Server.Core.Common.Workflow.CheckUserListExists;
using Server.Core.Lists.Workflow;
using Server.Core.Lists.Workflow.AddNewListItem;
using Server.Core.Lists.Workflow.CheckListItemExists;
using Server.Core.Lists.Workflow.MoveToListItem;
using Server.Core.Lists.Workflow.RemoveListItem;
using Server.Core.Lists.Workflow.UpdateListItem;
using Server.Core.Lists.Workflow.UpdateListItemCheck;

namespace Server.Core.RestApi.Controllers
{
    /// <summary>
    /// Контроллер для управления пунктами списков.
    /// </summary>
    public class ListItemController : WorkflowBaseController
    {
        /// <summary>
        /// Переопределение для создания workflow.
        /// </summary>
        /// <returns>Рабочий процесс.</returns>
        protected override WorkflowArea CreateWorkflow()
        {
            var workflow = new ListWorkflow();
            return workflow.Create();
        }

        /// <summary>
        /// Добавление нового пункта списка.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/list/item")]
        [Authorize]
        public async Task<MessageOutputBase> AddListItem([FromBody]AddNewListItemRequest request)
        {
            MessageOutputBase result = null;
            request.UserName = GetCurrentUser();
            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<CheckListExistsStep>()
                        .Add<AddNewListItemStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<ListNotFoundStep, ListNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<AddNewListItemStep, AddNewListItemParams>(add =>
                        {
                            result = add.Response;
                        });

                }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }

        /// <summary>
        /// Обновление пункта списка.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/list/item/update")]
        [Authorize]
        public async Task<MessageOutputBase> UpdateListItem([FromBody]UpdateListItemRequest request)
        {
            MessageOutputBase result = null;
            request.UserName = GetCurrentUser();
            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<CheckListExistsStep>()
                        .Add<CheckListItemExistsStep>()
                        .Add<UpdateListItemStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<ListNotFoundStep, ListNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<ListItemNotFoundStep, ListItemNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<UpdateListItemStep, UpdateListItemParams>(update =>
                        {
                            result = update.Response;
                        });

                }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }

        /// <summary>
        /// Удаление пункта списка.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/list/item/remove")]
        [Authorize]
        public async Task<MessageOutputBase> RemoveListItem([FromBody]RemoveListItemRequest request)
        {
            MessageOutputBase result = null;
            request.UserName = GetCurrentUser();
            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<CheckListExistsStep>()
                        .Add<CheckListItemExistsStep>()
                        .Add<RemoveListItemStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<ListNotFoundStep, ListNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<ListItemNotFoundStep, ListItemNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<RemoveListItemStep, RemoveListItemParams>(remove =>
                        {
                            result = remove.Response;
                        });

                }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }


        /// <summary>
        /// Перемещение пункта списка.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/list/item/moveto")]
        [Authorize]
        public async Task<MessageOutputBase> MoveToListItem([FromBody]MoveToListItemRequest request)
        {
            MessageOutputBase result = null;
            request.UserName = GetCurrentUser();
            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<MoveToListItemStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<ListNotFoundStep, ListNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<ListItemNotFoundStep, ListItemNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<MoveToListItemStep, MoveToListItemParams>(remove =>
                        {
                            result = remove.Response;
                        });

                }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }

        /// <summary>
        /// Обновление состояния пункта списка.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/list/item/updatecheck")]
        [Authorize]
        public async Task<MessageOutputBase> UpdateListItemCheck([FromBody]UpdateListItemCheckRequest request)
        {
            MessageOutputBase result = null;
            request.UserName = GetCurrentUser();
            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<CheckUserListExistsStep>()
                        .Add<CheckListItemExistsStep>()
                        .Add<UpdateListItemCheckStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<CheckUserListExistsStep, UserListNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<ListItemNotFoundStep, ListItemNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<UpdateListItemCheckStep, UpdateListItemCheckParams>(update =>
                        {
                            result = update.Response;
                        });

                }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }
    }
}