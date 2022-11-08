using Microsoft.AspNetCore.Mvc;
using TestApp.Module.Rfc.Application.GetRfcOverview;
using TestApp.ModuleIntegration.EntryPoint;

namespace TestApp.Api.Business.RequestForChange
{
    [ApiController]
    public class RequestsForChangesBatchOperationsController
    {
        [HttpGet("/requests-for-change/load-grid")]
        public async Task<IEnumerable<RfcOverviewDto>> GetRfcOverviewGridData([FromServices] IModuleDispatcher moduleDispatcher)
        {
            var query = new GetAllRfcsQuery();
            var allRfcs = await moduleDispatcher.Execute(query);

            return allRfcs;
        }
    }
}