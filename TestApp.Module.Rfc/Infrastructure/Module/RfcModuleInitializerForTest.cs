using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
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

        public async Task<bool> Run(bool databaseAlreadyCleared)
        {
            if(!databaseAlreadyCleared)
                await _context.Database.EnsureDeletedAsync();
            var created = await _context.Database.EnsureCreatedAsync();
            if (!created)
            {
                var databaseCreator = 
                    (RelationalDatabaseCreator) _context.Database.GetService<IDatabaseCreator>();
                await databaseCreator.CreateTablesAsync();
            }

            var first = RequestForChange.Create("RFC-1", "first RFC");
            var second = RequestForChange.Create("RFC-2", "second RFC");
            var third = RequestForChange.Create("RFC-3", "third RFC");
            var fourth = RequestForChange.Create("RFC-4", "fourth RFC");
            var fifth = RequestForChange.Create("RFC-5", "fifth RFC");

            first.Start();
            second.Start();
            second.WithDraw();
            third.Start();
            fifth.Start();
            fifth.Complete();

            await _context.RequestsForChange.AddRangeAsync(first, second, third, fourth, fifth);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}