using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApp.BuildingBlocks.Domain;

namespace TestApp.BuildingBlocks.Infrastructure.Domain.EntityFramework
{
    public abstract class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey("Id");
            builder.Ignore(requestForChange => requestForChange.DomainEvents);

            ConfigureCore(builder);
        }

        protected abstract void ConfigureCore(EntityTypeBuilder<T> builder);
    }
}