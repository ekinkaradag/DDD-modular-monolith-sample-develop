using TestApp.BuildingBlocks.Application.Commands;

namespace TestApp.Module.Rfc.Application.NewRfc
{
    public record NewRfcCommand : ICommand
    {
        public string Key { get; set; }
        public string Title { get; set; }
    }
}