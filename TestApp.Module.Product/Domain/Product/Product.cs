using TestApp.BuildingBlocks.Domain;
using TestApp.Module.Product.Domain.Product.Events;
using TestApp.Module.Product.Domain.Product.Rules;

namespace TestApp.Module.Product.Domain.Product
{
    internal class Product : Entity, IAggregateRoot
    {
        private const string Draft = "DRAFT";
        private const string ApprovedForUse = "APPROVED_FOR_USE";
        private const string Deprecated = "DEPRECATED";

        public string Key { get; }
        public string Title { get; }
        public string Version { get; }
        public string Status { get; private set; }

        private Product(
            string key,
            string title,
            string version)
        {
            Key = key;
            Title = title;
            Version = version;
            Status = Draft;

        }

        public static Product Create(string key,string title, string version)
        {
            CheckRule(new ProductRequiresKeyRule(key));
            CheckRule(new ProductRequiresTitleRule(title));
            CheckRule(new ProductRequiresVersionRule(version));
        
            var product = new Product
            (
                key,
                title,
                version
            );

            return product;
        }

        public void Approve()
        {
            CheckRule(new ProductCanOnlyBeApprovedWhenInDraftRule(Status));
        
            Status = ApprovedForUse;
        
            AddDomainEvent(new ProductApprovedEvent(Key));
        }

        public void Deprecate()
        {
            CheckRule(new ProductCanOnlyBeDeprecatedWhenApprovedForUseRule(Status));
        
            Status = Deprecated;
        
            AddDomainEvent(new ProductDeprecatedEvent(Key));
        }
        
        public void BackToDraft()
        {
            CheckRule(new ProductCanOnlyBeDraftWhenApprovedForUseRule(Status));
        
            Status = Draft;
        
            AddDomainEvent(new ProductDraftedEvent(Key));
        }
    }
}