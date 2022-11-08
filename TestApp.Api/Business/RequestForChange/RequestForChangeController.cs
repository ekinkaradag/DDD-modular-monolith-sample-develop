using Microsoft.AspNetCore.Mvc;
using TestApp.Module.Rfc.Application.GetRfcOverview;
using TestApp.Module.Rfc.Application.NewRfc;
using TestApp.Module.Rfc.Application.WithdrawRfc;
using TestApp.ModuleIntegration.EntryPoint;

namespace TestApp.Api.Business.RequestForChange
{
    [ApiController]
    public class RequestForChangeController
    {
        [HttpGet("/requests-for-change")]
        public async Task<IEnumerable<RfcOverviewDto>> GetAllRequestsForChange([FromServices] IModuleDispatcher moduleDispatcher)
        {
            var query = new GetAllRfcsQuery();
            var allRfcs = await moduleDispatcher.Execute(query);

            return allRfcs;
        }
    
        [HttpPost("/requests-for-change/")]
        public async Task NewRequestForChange(NewRfcCommand command, [FromServices] IModuleDispatcher moduleDispatcher)
        {
            await moduleDispatcher.Execute(command);
        }
    
        [HttpPost("/requests-for-change/withdraw")]
        public async Task WithdrawRequestForChange(WithdrawRfcCommand command, [FromServices] IModuleDispatcher moduleDispatcher)
        {
            await moduleDispatcher.Execute(command);
        }
    }
}