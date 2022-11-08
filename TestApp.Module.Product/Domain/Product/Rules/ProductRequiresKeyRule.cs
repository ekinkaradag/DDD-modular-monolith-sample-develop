using TestApp.BuildingBlocks.Domain;

namespace TestApp.Module.Product.Domain.Product.Rules
{
    internal class ProductRequiresKeyRule : IBusinessRule
    {
        private readonly string _key;

        public ProductRequiresKeyRule(string key)
        {
            _key = key;
        }

        public bool IsBroken()
        {
            return string.IsNullOrWhiteSpace(_key);
        }

        public string Message => "A product must have a key";
    }
}