using System;
using TestApp.BuildingBlocks.Application.Commands;

namespace TestApp.Module.Rfc.Application.WithdrawRfc
{
    public record WithdrawRfcCommand(Guid RfcId) : ICommand
    {
        public Guid RfcId { get; } = RfcId;
    }
}