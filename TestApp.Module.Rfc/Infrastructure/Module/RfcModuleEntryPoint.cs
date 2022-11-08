using Autofac;
using Autofac.Core;
using Microsoft.EntityFrameworkCore;
using TestApp.BuildingBlocks.Infrastructure.Database;
using TestApp.BuildingBlocks.Module;
using TestApp.Module.Rfc.Infrastructure.EntityFramework;

namespace TestApp.Module.Rfc.Infrastructure.Module
{
    internal class RfcModuleDependencyInjectionEntryPoint : DependencyInjectionEntryPoint
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RequestForChangeContext>()
                .WithParameter(
                    new ResolvedParameter(
                        (info, context) => info.ParameterType == typeof(DbContextOptions<RequestForChangeContext>),
                        (info, context) =>
                        {
                            var connectionFactory = context.Resolve<ISqlConnectionFactory>();
                            var dbContextOptionsBuilder = new DbContextOptionsBuilder<RequestForChangeContext>();
                            
                            dbContextOptionsBuilder.UseSqlServer(connectionFactory.GetConnectionString());
                            
                            return dbContextOptionsBuilder.Options;
                        }))
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}