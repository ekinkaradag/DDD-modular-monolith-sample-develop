using TestApp.BuildingBlocks.Domain;

namespace TestApp.Module.Product.Domain.Product.Rules
{
    internal class ProductCanOnlyBeDeprecatedWhenApprovedForUseRule : IBusinessRule
    {
        private readonly string _currentStatus;

        public ProductCanOnlyBeDeprecatedWhenApprovedForUseRule(string currentStatus)
        {
            _currentStatus = currentStatus;
        }

        public bool IsBroken()
        {
            return _currentStatus != "APPROVED_FOR_USE";
        }

        public string Message => "Product can only be deprecated when approved for use";
    }
}