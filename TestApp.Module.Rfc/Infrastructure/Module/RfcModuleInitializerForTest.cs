using System.Threading.Tasks;
using TestApp.BuildingBlocks.Module;
using TestApp.Module.Rfc.Domain.RequestForChange;
using TestApp.Module.Rfc.Infrastructure.EntityFramework;

namespace TestApp.Module.Rfc.Infrastructure.Module
{
    internal class RfcModuleInitializerForTest : IModuleInitializer
    {
        private readonly RequestForChangeContext _context;

        public RfcModuleInitializerForTest(RequestForChangeContext context)
        {
            _context = context;
        }

        public async Task Run()
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.Database.EnsureCreatedAsync();

            var first = RequestForChange.Create("RFC-1", "first RFC");
            var second = RequestForChange.Create("RFC-2", "second RFC");
            var third = RequestForChange.Create("RFC-3", "third RFC");
            var fourth = RequestForChange.Create("RFC-4", "fourth RFC");

            first.Start();
            second.Start();
            second.WithDraw();
            third.Start();

            await _context.RequestsForChange.AddRangeAsync(first,second,third,fourth);
            await _context.SaveChangesAsync();
        }
    }
}