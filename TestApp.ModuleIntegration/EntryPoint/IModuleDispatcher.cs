using System.Threading.Tasks;
using TestApp.BuildingBlocks.Application.Commands;
using TestApp.BuildingBlocks.Application.Queries;

namespace TestApp.ModuleIntegration.EntryPoint
{
    public interface IModuleDispatcher
    {
        Task<TResult> Execute<TResult>(ICommand<TResult> command);

        Task Execute(ICommand command);

        Task<TResult> Execute<TResult>(IQuery<TResult> query);
    }
}