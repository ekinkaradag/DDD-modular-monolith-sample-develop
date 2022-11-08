using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Features.Variance;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using TestApp.BuildingBlocks.Application.Commands;
using TestApp.BuildingBlocks.Application.Queries;
using TestApp.BuildingBlocks.Domain;
using TestApp.BuildingBlocks.Infrastructure.Database;
using TestApp.BuildingBlocks.Module;
using TestApp.ModuleIntegration.ModuleDataAccess;
using TestApp.ModuleIntegration.Processing;

namespace TestApp.ModuleIntegration.EntryPoint
{
    public class ModuleIntegrationModule : Autofac.Module
    {
        private readonly Assembly[] _moduleAssemblies;
        private readonly string _connectionString;

        public ModuleIntegrationModule(string connectionString, Assembly[] moduleAssemblies)
        {
            _connectionString = connectionString;
            _moduleAssemblies = moduleAssemblies;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSource(new ScopedContravariantRegistrationSource(
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
                typeof(IValidator<>),
                typeof(IRepository<>)));
        
            RegisterModuleMediator(builder);

            RegisterModules(builder, _moduleAssemblies);

            RegisterModuleDataAccess(builder);
        }

        private void RegisterModuleDataAccess(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnectionFactory>()
                .WithParameter(new TypedParameter(typeof(string), _connectionString))
                .As<ISqlConnectionFactory>();
        }

        private static void RegisterRepositories(ContainerBuilder builder, Assembly[] moduleAssemblies)
        {
            var openTypes = new[]
            {
                typeof(IRepository<>)
            };

            foreach (var openType in openTypes)
            {
                builder
                    .RegisterAssemblyTypes(moduleAssemblies)
                    .AsClosedTypesOf(openType)
                    .AsImplementedInterfaces();
            }
        }

        private static void RegisterModules(ContainerBuilder builder, Assembly[] moduleAssemblies)
        {
            RegisterInitializers(builder, moduleAssemblies);
            RegisterCommandsQueriesAndEvents(builder, moduleAssemblies);
            RegisterCommandValidators(builder, moduleAssemblies);
            RegisterHandlersForCommandsQueriesAndEvents(builder, moduleAssemblies);
            RegisterRepositories(builder, moduleAssemblies);

            builder.RegisterAssemblyModules<DependencyInjectionEntryPoint>(moduleAssemblies);
        }

        private static void RegisterInitializers(ContainerBuilder builder, Assembly[] moduleAssemblies)
        {
            builder
                .RegisterAssemblyTypes(moduleAssemblies)
                .AssignableTo(typeof(IModuleInitializer))
                .AsImplementedInterfaces();
        }

        private static void RegisterCommandsQueriesAndEvents(ContainerBuilder builder, Assembly[] assemblies)
        {
            var openTypes = new[]
            {
                typeof(ICommand<>),
                typeof(IQuery<>),
            };

            var closedTypes = new[]
            {
                typeof(ICommand)
            };

            foreach (var openType in openTypes)
            {
                builder
                    .RegisterAssemblyTypes(assemblies)
                    .AsClosedTypesOf(openType)
                    .AsImplementedInterfaces();
            }

            foreach (var closedType in closedTypes)
            {
                builder
                    .RegisterAssemblyTypes(assemblies)
                    .AssignableTo(closedType)
                    .AsImplementedInterfaces();
            }
        }

        private static void RegisterCommandValidators(ContainerBuilder builder, Assembly[] assemblies)
        {
            var openTypes = new[]
            {
                typeof(IValidator<>)
            };

            foreach (var openType in openTypes)
            {
                builder
                    .RegisterAssemblyTypes(assemblies)
                    .AsClosedTypesOf(openType)
                    .AsImplementedInterfaces();
            }
        }

        private static void RegisterHandlersForCommandsQueriesAndEvents(ContainerBuilder builder, Assembly[] assemblies)
        {
            var openTypes = new[]
            {
                typeof(IRequestHandler<>),
                typeof(IRequestHandler<,>),
                typeof(IRequestExceptionHandler<,,>),
                typeof(IRequestExceptionAction<,>),
                typeof(INotificationHandler<>),
            };

            foreach (var openType in openTypes)
            {
                builder
                    .RegisterAssemblyTypes(assemblies)
                    .AsClosedTypesOf(openType)
                    // when having a single class implementing several handler types
                    // this call will cause a handler to be called twice
                    // in general you should try to avoid having a class implementing for instance `IRequestHandler<,>` and `INotificationHandler<>`
                    // the other option would be to remove this call
                    // see also https://github.com/jbogard/MediatR/issues/462
                    .AsImplementedInterfaces();
            }
        }

        //see also https://github.com/jbogard/MediatR/blob/master/samples/MediatR.Examples.Autofac/Program.cs#L22
        private static void RegisterModuleMediator(ContainerBuilder builder)
        {
            builder.RegisterType<MediatingModuleDispatcher>().As<IModuleDispatcher>();

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            //register pipeline behaviors
            // It appears Autofac returns the last registered types first
            // see https://github.com/jbogard/MediatR/wiki/Behaviors for issue with behaviors and autofac
            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidateCommandBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(LogRequestBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestExceptionActionProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestExceptionProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }

        //see https://github.com/jbogard/MediatR/issues/128 for issue with contravariant registration and mediatr
        private class ScopedContravariantRegistrationSource : IRegistrationSource
        {
            private readonly IRegistrationSource _source = new ContravariantRegistrationSource();
            private readonly List<Type> _types = new List<Type>();

            public ScopedContravariantRegistrationSource(params Type[] types)
            {
                if (types == null)
                {
                    throw new ArgumentNullException(nameof(types));
                }

                if (!types.All(x => x.IsGenericTypeDefinition))
                {
                    throw new ArgumentException("Supplied types should be generic type definitions");
                }

                _types.AddRange(types);
            }

            public IEnumerable<IComponentRegistration> RegistrationsFor(
                Service service,
                Func<Service, IEnumerable<ServiceRegistration>> registrationAccessor)
            {
                var components = _source.RegistrationsFor(service, registrationAccessor);
                foreach (var c in components)
                {
                    var defs = c.Target.Services
                        .OfType<TypedService>()
                        .Select(x => x.ServiceType.GetGenericTypeDefinition());

                    if (defs.Any(_types.Contains))
                    {
                        yield return c;
                    }
                }
            }

            public bool IsAdapterForIndividualComponents => _source.IsAdapterForIndividualComponents;
        }
    }
}