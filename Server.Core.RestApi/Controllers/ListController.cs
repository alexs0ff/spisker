using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Core.Common.Messages;
using Server.Core.Common.Workflow;
using Server.Core.Common.Workflow.CheckListExists;
using Server.Core.Common.Workflow.CheckUserExists;
using Server.Core.Common.Workflow.CheckUserListExists;
using Server.Core.Lists.Workflow;
using Server.Core.Lists.Workflow.AddNewList;
using Server.Core.Lists.Workflow.GetUserFeed;
using Server.Core.Lists.Workflow.GetUserLists;
using Server.Core.Lists.Workflow.RemoveList;
using Server.Core.Lists.Workflow.UpdateList;
using Server.Core.Lists.Workflow.UpdateListCheckItemKind;
using Server.Core.Lists.Workflow.UpdateListKind;
using Server.Core.Lists.Workflow.UpdatePublished;

namespace Server.Core.RestApi.Controllers
{
    /// <summary>
    /// Контроллер для управления списками.
    /// </summary>
    public class ListController: WorkflowBaseController
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
        /// Получение списков определенного пользователя.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpGet]
        [Route("api/list")]
        public async Task<MessageOutputBase> Lists(GetUserListsRequest request)
        {
            MessageOutputBase result = null;

            request.ForUserName = GetCurrentUser();
            request.ForUserId = GetCurrentUserId();

            await Execute(flow =>
            {
                flow.StartRegisterFlow()
                .Add<CheckUserExistsStep>()
                .Add<FetchUserListsStep>();

                flow.
                When<UserNotFoundStep, UserNotFoundParams>((notFound) =>
                {
                    result = notFound.Response;
                })
                .When<FetchUserListsStep, FetchUserListsParams>((fetch) =>
                    {
                        result = fetch.ListsResponse;
                    });

            }, request, 
            error =>
            {
                result = error;
            });

            return result;
        }

        /// <summary>
        /// Получение ленты определенного пользователя.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpGet]
        [Route("api/feed")]
        [Authorize]
        public async Task<MessageOutputBase> Feed(GetUserFeedRequest request)
        {
            MessageOutputBase result = null;
            request.UserName = GetCurrentUser();

            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<GetUserFeedStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>((notFound) =>
                        {
                            result = notFound.Response;
                        })
                        .When<GetUserFeedStep, GetUserFeedParams>((fetch) =>
                        {
                            result = fetch.ListsResponse;
                        });

                }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }

        /// <summary>
        /// Добавление нового списка.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/list")]
        [Authorize]
        public async Task<MessageOutputBase> AddList([FromBody]AddNewListRequest request)
        {
            MessageOutputBase result = null;
            request.UserName = GetCurrentUser();
            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<AddNewListStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>((notFound) =>
                        {
                            result = notFound.Response;
                        })
                        .When<AddNewListStep, AddNewListParams>(( add) =>
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
        /// Обновление списка.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/list/update")]
        [Authorize]
        public async Task<MessageOutputBase> UpdateList([FromBody]UpdateListRequest request)
        {
            MessageOutputBase result = null;
            request.UserName = GetCurrentUser();
            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<CheckListExistsStep>()
                        .Add<UpdateListStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .
                        When<ListNotFoundStep, ListNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<UpdateListStep, UpdateListParams>(update =>
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
        /// Удаление списка.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/list/remove")]
        [Authorize]
        public async Task<MessageOutputBase> RemoveList([FromBody]RemoveListRequest request)
        {
            MessageOutputBase result = null;
            request.UserName = GetCurrentUser();
            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<CheckListExistsStep>()
                        .Add<RemoveListStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .
                        When<ListNotFoundStep, ListNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<RemoveListStep, RemoveListParams>(remove =>
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
        /// Изменение типа списка.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/list/updatekind")]
        [Authorize]
        public async Task<MessageOutputBase> UpdateListKind([FromBody]UpdateListKindRequest request)
        {
            MessageOutputBase result = null;
            request.UserName = GetCurrentUser();
            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<CheckUserListExistsStep>()
                        .Add<UpdateListKindStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .
                        When<UserListNotFoundStep, UserListNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<UpdateListKindStep, UpdateListKindParams>(update =>
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
        /// Изменение типа списка.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/list/updatepublished")]
        [Authorize]
        public async Task<MessageOutputBase> UpdatePublished([FromBody]UpdatePublishedRequest request)
        {
            MessageOutputBase result = null;
            request.UserName = GetCurrentUser();
            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<CheckUserListExistsStep>()
                        .Add<UpdatePublishedStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .
                        When<UserListNotFoundStep, UserListNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<UpdatePublishedStep, UpdatePublishedParams>(update =>
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
        /// Изменение типа списка.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/list/updatecheckitemkind")]
        [Authorize]
        public async Task<MessageOutputBase> UpdateListCheckItemKind([FromBody]UpdateListCheckItemKindRequest request)
        {
            MessageOutputBase result = null;
            request.UserName = GetCurrentUser();
            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<CheckUserListExistsStep>()
                        .Add<UpdateListCheckItemKindStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .
                        When<UserListNotFoundStep, UserListNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<UpdateListCheckItemKindStep, UpdateListCheckItemKindParams>(update =>
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