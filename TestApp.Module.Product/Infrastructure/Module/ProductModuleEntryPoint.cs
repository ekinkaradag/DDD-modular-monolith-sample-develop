using Autofac;
using Autofac.Core;
using Microsoft.EntityFrameworkCore;
using TestApp.BuildingBlocks.Infrastructure.Database;
using TestApp.BuildingBlocks.Module;
using TestApp.Module.Product.Infrastructure.EntityFramework;

namespace TestApp.Module.Product.Infrastructure.Module
{
    internal class ProductModuleDependencyInjectionEntryPoint : DependencyInjectionEntryPoint
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductContext>()
                .WithParameter(
                    new ResolvedParameter(
                        (info, context) => info.ParameterType == typeof(DbContextOptions<ProductContext>),
                        (info, context) =>
                        {
                            var connectionFactory = context.Resolve<ISqlConnectionFactory>();
                            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ProductContext>();
                            
                            dbContextOptionsBuilder.UseSqlServer(connectionFactory.GetConnectionString());
                            
                            return dbContextOptionsBuilder.Options;
                        }))
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}