using System.Threading.Tasks;

namespace TestApp.BuildingBlocks.Module
{
    public interface IModuleInitializer
    {
        Task Run();
    }
}