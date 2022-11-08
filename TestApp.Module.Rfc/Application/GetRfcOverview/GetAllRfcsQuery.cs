using System.Collections.Generic;
using TestApp.BuildingBlocks.Application.Queries;

namespace TestApp.Module.Rfc.Application.GetRfcOverview
{
    public record GetAllRfcsQuery : IQuery<IEnumerable<RfcOverviewDto>>;
}