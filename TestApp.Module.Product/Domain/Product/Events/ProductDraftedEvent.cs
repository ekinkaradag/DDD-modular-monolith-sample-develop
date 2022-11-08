using TestApp.BuildingBlocks.Domain;

namespace TestApp.Module.Product.Domain.Product.Events
{
    internal class ProductDraftedEvent : IDomainEvent
    {
        public string ProductKey { get; }

        public ProductDraftedEvent(string productKey)
        {
            ProductKey = productKey;
        }
    }
}