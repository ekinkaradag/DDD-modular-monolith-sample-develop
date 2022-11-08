using Autofac;
using Autofac.Core;
using IoCore.SharedReadKernel.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using TestApp.BuildingBlocks.Infrastructure.Database;
using TestApp.BuildingBlocks.Module;

namespace IoCore.SharedReadKernel.Infrastructure.Module
{
    // ReSharper disable once UnusedType.Global
    internal class ReadKernelDependencyInjectionEntryPoint : DependencyInjectionEntryPoint
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ReadContext>()
                .WithParameter(
                    new ResolvedParameter(
                        (info, context) => info.ParameterType == typeof(DbContextOptions<ReadContext>),
                        (info, context) =>
                        {
                            var connectionFactory = context.Resolve<ISqlConnectionFactory>();
                            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ReadContext>();

                            dbContextOptionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                            
                            dbContextOptionsBuilder.UseSqlServer(connectionFactory.GetConnectionString());
                            
                            return dbContextOptionsBuilder.Options;
                        }))
                .AsSelf()
                .As<IReadModelAccess>()
                .InstancePerLifetimeScope();
        }
    }
}