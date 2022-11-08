using TestApp.BuildingBlocks.Domain;

namespace TestApp.Module.Product.Domain.Product.Rules
{
    internal class ProductRequiresTitleRule : IBusinessRule
    {
        private readonly string _title;

        public ProductRequiresTitleRule(string title)
        {
            _title = title;
        }

        public bool IsBroken()
        {
            return string.IsNullOrWhiteSpace(_title);
        }

        public string Message => "A product must have a title";
    }
}