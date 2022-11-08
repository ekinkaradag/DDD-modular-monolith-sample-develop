using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestApp.BuildingBlocks.Infrastructure.Domain.EntityFramework;
using TestApp.Module.Rfc.Domain.RequestForChange;

namespace TestApp.Module.Rfc.Infrastructure.EntityFramework
{
    internal class RequestForChangeConfiguration : EntityTypeConfiguration<RequestForChange>
    {
        protected override void ConfigureCore(EntityTypeBuilder<RequestForChange> builder)
        {
            builder.ToTable("RequestForChange");
            builder.HasAlternateKey(requestForChange => requestForChange.Key);
            builder.Property(requestForChange => requestForChange.Title).IsRequired();
            builder.Property(requestForChange => requestForChange.DateRaised).IsRequired();
            builder.Property(requestForChange => requestForChange.Status).IsRequired();
        }
    }
}