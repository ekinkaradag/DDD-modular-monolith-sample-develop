using TestApp.BuildingBlocks.Domain;

namespace TestApp.Module.Product.Domain.Product.Rules
{
    internal class ProductCanOnlyBeApprovedWhenInDraftRule : IBusinessRule
    {
        private readonly string _currentStatus;

        public ProductCanOnlyBeApprovedWhenInDraftRule(string currentStatus)
        {
            _currentStatus = currentStatus;
        }

        public bool IsBroken()
        {
            return _currentStatus != "DRAFT";
        }

        public string Message => "Product can only be approved when in draft";
    }
}