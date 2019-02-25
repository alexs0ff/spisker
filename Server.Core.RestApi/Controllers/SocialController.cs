using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Core.Common.Messages;
using Server.Core.Common.Workflow;
using Server.Core.Common.Workflow.CheckListExists;
using Server.Core.Common.Workflow.CheckUserExists;
using Server.Core.Social.Workflow;
using Server.Core.Social.Workflow.FindUsers;
using Server.Core.Social.Workflow.GetFollowings;
using Server.Core.Social.Workflow.GetProfile;
using Server.Core.Social.Workflow.GetWhoFollow;
using Server.Core.Social.Workflow.RepostList;
using Server.Core.Social.Workflow.SetLikeList;
using Server.Core.Social.Workflow.StartFollowing;
using Server.Core.Social.Workflow.StopFollowing;
using Server.Core.Social.Workflow.UnsetLikeList;

namespace Server.Core.RestApi.Controllers
{
    /// <summary>
    /// Контроллер операций над социальными параметрами.
    /// </summary>
    public class SocialController: WorkflowBaseController
    {
        /// <summary>
        /// Переопределение для создания workflow.
        /// </summary>
        /// <returns>Рабочий процесс.</returns>
        protected override WorkflowArea CreateWorkflow()
        {
            var workflow = new SocialWorkflow();

            return workflow.Create();
        }

        /// <summary>
        /// Получение профиля определенного пользователя.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpGet]
        [Route("api/profile")]
        public async Task<MessageOutputBase> Profile(GetProfileRequest request)
        {
            MessageOutputBase result = null;

            request.CurrentUserName = GetCurrentUser();

            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<GetProfileStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<GetProfileStep, GetProfileParams>(fetch =>
                        {
                            result = fetch.Response;
                        });

                }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }


        
        /// <summary>
        /// Начало подписки за пользователем.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/startfollowing")]
        [Authorize]
        public async Task<MessageOutputBase> StartFollowing([FromBody]StartFollowingRequest request)
        {
            MessageOutputBase result = null;
            request.UserName = GetCurrentUser();

            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<StartFollowingStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<StartFollowingStep, StartFollowingParams>(start =>
                        {
                            result = start.Response;
                        });

                }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }

        /// <summary>
        /// Остановка подписки на пользователя.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/stopfollowing")]
        [Authorize]
        public async Task<MessageOutputBase> StopFollowing([FromBody]StopFollowingRequest request)
        {
            MessageOutputBase result = null;
            request.UserName = GetCurrentUser();

            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<StopFollowingStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<StopFollowingStep, StopFollowingParams>(stop =>
                        {
                            result = stop.Response;
                        });

                }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }

        /// <summary>
        /// Установка лайка на список.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/setlikelist")]
        [Authorize]
        public async Task<MessageOutputBase> SetLikeList([FromBody]SetLikeListRequest request)
        {
            MessageOutputBase result = null;
            request.UserName = GetCurrentUser();

            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<CheckListExistsStep>()
                        .Add<SetLikeListStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<ListNotFoundStep, ListNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<SetLikeListStep, SetLikeListParams>(setLike =>
                        {
                            result = setLike.Response;
                        });

                }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }

        /// <summary>
        /// Отзыв лайка на список.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/unsetlikelist")]
        [Authorize]
        public async Task<MessageOutputBase> UnsetLikeList([FromBody]UnsetLikeListRequest request)
        {
            MessageOutputBase result = null;
            request.UserName = GetCurrentUser();

            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<CheckListExistsStep>()
                        .Add<UnsetLikeListStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<ListNotFoundStep, ListNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<UnsetLikeListStep, UnsetLikeListParams>(unsetLike =>
                        {
                            result = unsetLike.Response;
                        });

                }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }


        /// <summary>
        /// Репост списка.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpPost]
        [Route("api/repostlist")]
        [Authorize]
        public async Task<MessageOutputBase> RepostList([FromBody]RepostListRequest request)
        {
            MessageOutputBase result = null;
            request.UserName = GetCurrentUser();

            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<CheckListExistsStep>()
                        .Add<RepostListStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<ListNotFoundStep, ListNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<RepostListStep, RepostListParams>(repost =>
                        {
                            result = repost.Response;
                        });

                }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }

        /// <summary>
        /// Получение списка пользователей по вхождению в строку запроса.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpGet]
        [Route("api/findusers")]
        public async Task<MessageOutputBase> FindUsers(FindUsersRequest request)
        {
            MessageOutputBase result = null;

            request.CurrentUserName = GetCurrentUser();

            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<FindUsersStep>();
                        

                    flow.When<FindUsersStep, FindUsersParams>(fetch =>
                        {
                            result = fetch.Response;
                        });

                }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }


        /// <summary>
        /// Получение профилей последователей следователей.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpGet]
        [Route("api/getfollowers")]
        public async Task<MessageOutputBase> GetFollowers(GetWhoFollowRequest request)
        {
            MessageOutputBase result = null;

            request.CurrentUserName = GetCurrentUser();

            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<GetWhoFollowStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<GetWhoFollowStep, GetWhoFollowParams>(fetch =>
                        {
                            result = fetch.Response;
                        });

                }, request,
                error =>
                {
                    result = error;
                });

            return result;
        }

        /// <summary>
        /// Получение профилей последователей на которых осуществлена подписка.
        /// </summary>
        /// <param name="request">Запрос.</param>
        /// <returns>Ответ.</returns>
        [HttpGet]
        [Route("api/getfollowings")]
        public async Task<MessageOutputBase> GetFollowings(GetFollowingsRequest request)
        {
            MessageOutputBase result = null;

            request.CurrentUserName = GetCurrentUser();

            await Execute(flow =>
                {
                    flow.StartRegisterFlow()
                        .Add<CheckUserExistsStep>()
                        .Add<GetFollowingsStep>();

                    flow.
                        When<UserNotFoundStep, UserNotFoundParams>(notFound =>
                        {
                            result = notFound.Response;
                        })
                        .When<GetFollowingsStep, GetFollowingsParams>(fetch =>
                        {
                            result = fetch.Response;
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