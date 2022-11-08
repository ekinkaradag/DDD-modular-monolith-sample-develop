using TestApp.BuildingBlocks.Domain;

namespace TestApp.Module.Product.Domain.Product.Events
{
    internal class ProductDeprecatedEvent : IDomainEvent
    {
        public string ProductKey { get; }
        public ProductDeprecatedEvent(string productKey)
        {
            ProductKey = productKey;
        }
    }
}