using TestApp.BuildingBlocks.Domain;

namespace TestApp.Module.Product.Domain.Product.Rules
{
    internal class ProductCanOnlyBeDraftWhenApprovedForUseRule : IBusinessRule
    {
        private readonly string _currentStatus;

        public ProductCanOnlyBeDraftWhenApprovedForUseRule(string currentStatus)
        {
            _currentStatus = currentStatus;
        }

        public bool IsBroken()
        {
            return _currentStatus != "APPROVED_FOR_USE";
        }

        public string Message => "Product can only be draft when approved for use";
    }
}