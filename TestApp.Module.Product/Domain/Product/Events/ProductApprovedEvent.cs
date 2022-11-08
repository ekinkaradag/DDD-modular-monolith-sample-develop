using TestApp.BuildingBlocks.Domain;

namespace TestApp.Module.Product.Domain.Product.Events
{
    internal class ProductApprovedEvent : IDomainEvent
    {
        public string ProductKey { get; }

        public ProductApprovedEvent(string productKey)
        {
            ProductKey = productKey;
        }
    }
}