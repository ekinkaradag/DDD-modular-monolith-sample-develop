using System;
using TestApp.BuildingBlocks.Application.Commands;

namespace TestApp.Module.Product.Application.ApproveProduct
{
    public record ApproveProductCommand(Guid ProductId) : ICommand
    {
        public Guid ProductId { get; } = ProductId;
    }
}