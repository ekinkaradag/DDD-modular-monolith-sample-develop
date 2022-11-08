using IoCore.SharedReadKernel.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IoCore.SharedReadKernel.RequestForChange.EntityConfigurations
{
    internal class RequestForChangeConfiguration : EntityTypeConfiguration<RequestForChange>
    {
        protected override void ConfigureCore(EntityTypeBuilder<RequestForChange> builder)
        {
            builder.ToTable("RequestForChange", "rfc");
        }
    }
}