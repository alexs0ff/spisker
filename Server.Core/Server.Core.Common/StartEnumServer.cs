using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Server.Core.Common.Entities;
using Server.Core.Common.Files;
using Server.Core.Common.GraphQL;
using Server.Core.Common.Logger;
using Server.Core.Common.Repositories;
using Server.Core.Common.Services;
using Server.Core.Common.Settings;
using Server.Core.Common.Settings.Files;
using Server.Core.Common.Settings.Mail;

namespace Server.Core.Common
{
    /// <summary>
    /// Сервер приложения.
    /// </summary>
    public class StartEnumServer
    {
        #region Singleton

        /// <summary>
        ///   Объект синхронизации.
        /// </summary>
        private static readonly object _syncRoot = new object();

        /// <summary>
        ///   Инстанс сервера.
        /// </summary>
        private static volatile StartEnumServer _startEnumServer;

        /// <summary>
        ///   Ioc контейнер.
        /// </summary>
        private static IContainer _container;
        

        /// <summary>
        ///   Установка контейнера типов IoC.
        /// </summary>
        /// <param name="configuration"> Конфигурация. </param>
        public static void SetConfiguration(IContainer configuration)
        {
            _container = configuration;
        }

        /// <summary>
        ///   Инициализирует новый экземпляр класса <see cref="T:Romontinka.Server.Core.RemontinkaServer" /> .
        /// </summary>
        private StartEnumServer()
        {
            
        }

        /// <summary>
        /// Обеспечивает проверку на возможность решения типа.
        /// </summary>
        /// <typeparam name="T">Тип для резолвинга.</typeparam>
        /// <returns>Инициализированный объект типа или null.</returns>
        private T SaveResolve<T>() where T : class
        {
            T result = null;

            if (_container.IsRegistered<T>())
            {
                result = _container.Resolve<T>();
            } //if

            return result;
        }

        /// <summary>
        /// Получает инстанс сервера.
        /// </summary>
        /// <returns></returns>
        public static StartEnumServer Instance
        {
            get
            {
                if (_startEnumServer == null)
                {
                    lock (_syncRoot)
                    {
                        if (_startEnumServer == null)
                        {
                            if (_container == null)
                            {
                                throw new InvalidOperationException(
                                    "Доступ к сервисам не возможен, контейнер типов в текущем контексте не инициализирован.");
                            }

                            _startEnumServer = new StartEnumServer();
                        }
                    }
                }

                return _startEnumServer;
            }
        }

        #endregion


        /// <summary>
        /// Проверяет на то что сервис проинициализирован.
        /// </summary>
        /// <typeparam name="T">Проверяемый тип.</typeparam>
        private void CheckService<T>(object serviceInstance)
        {
            if (serviceInstance == null)
            {
                throw new ServiceIsNotAvailableException(typeof(T).Name,
                    string.Format(
                        "Сервис '{0}' не инициализирован, проверьте настройки контейнера",
                        typeof(T).Name));
            } //if
        }


        /// <summary>
        /// Получает репозиторий типа Crud.
        /// </summary>
        /// <typeparam name="TRepository">Тип репозитория.</typeparam>
        /// <typeparam name="TEntity">Тип сущности.</typeparam>
        /// <typeparam name="TKey">Тип ключа сущности.</typeparam>
        /// <returns></returns>
        public TRepository GetRepository<TRepository, TEntity, TKey>()
            where TKey : struct
            where TEntity : EntityBase<TKey>
            where TRepository : class, ICrudRepository<TEntity, TKey>
        {
            var repository = SaveResolve<TRepository>();

            CheckService<TRepository>(repository);

            return repository;
        }

        /// <summary>
        /// Получает репозиторий обобщенного типа.
        /// </summary>
        /// <typeparam name="TRepository">Тип репозитория.</typeparam>
        /// <returns></returns>
        public TRepository GetRepository<TRepository>()
            where TRepository : class, IRepository
        {
            var repository = SaveResolve<TRepository>();

            CheckService<TRepository>(repository);

            return repository;
        }

        /// <summary>
        /// Получает настройки обобщенного типа.
        /// </summary>
        /// <typeparam name="TSettings">Тип репозитория.</typeparam>
        /// <returns></returns>
        public TSettings GetSettings<TSettings>()
            where TSettings : class, ISettings
        {
            var settings = SaveResolve<TSettings>();

            CheckService<TSettings>(settings);

            return settings;
        }

        /// <summary>
        /// Получает объект через контейнер.
        /// </summary>
        /// <returns></returns>
        public T Resolve<T>()
            where T:class
        {
            var repository = SaveResolve<T>();

            CheckService<T>(repository);

            return repository;
        }

        /// <summary>
        /// Получает серивс обобщенного типа.
        /// </summary>
        /// <typeparam name="TService">Тип серивиса.</typeparam>
        /// <returns></returns>
        public TService GetService<TService>()
            where TService : class, IServiceBase
        {
            var service = SaveResolve<TService>();

            CheckService<TService>(service);

            return service;
        }

        /// <summary>
        /// Получение Mapper.
        /// </summary>
        /// <returns>Инстанс для преобразования типов.</returns>
        public IMapper GetMapper()
        {
            var mapper = SaveResolve<IMapper>();

            CheckService<IMapper>(mapper);

            return mapper;
        }

        /// <summary>
        /// Получает логгер.
        /// </summary>
        /// <returns>Логгер.</returns>
        public IServerLogger GetLogger()
        {
            var logger = SaveResolve<IServerLogger>();

            CheckService<IServerLogger>(logger);

            return logger;
        }

        /// <summary>
        /// Получение сервиса по операциям с файлами.
        /// </summary>
        /// <returns>Инстанс для работы с файлами.</returns>
        public IFileStoreService GetFileStore()
        {
            var fileStore = SaveResolve<IFileStoreService>();

            CheckService<IFileStoreService>(fileStore);

            return fileStore;
        }

        /// <summary>
        /// Получает службу отправки email.
        /// </summary>
        /// <returns>Служба отправки email.</returns>
        public IMailingService GetMailingService()
        {
            var service = SaveResolve<IMailingService>();

            CheckService<IMailingService>(service);

            return service;
        }

        /// <summary>
        /// Получает резолвер для GraphQL.
        /// </summary>
        /// <typeparam name="TResolver">Тип резолвера.</typeparam>
        /// <returns>Резолвер</returns>
        public TResolver GetResolver<TResolver, TSourceType>()
            where TResolver : class,ITypeResolver<TSourceType>
        {
            var resolver = SaveResolve<TResolver>();

            CheckService<TResolver>(resolver);

            return resolver;
        }
    }
}
