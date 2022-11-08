using TestApp.BuildingBlocks.Domain;

namespace TestApp.Module.Rfc.Domain.RequestForChange.Rules
{
    internal class RequestForChangeRequiresKeyRule : IBusinessRule
    {
        private readonly string _key;

        public RequestForChangeRequiresKeyRule(string key)
        {
            _key = key;
        }

        public bool IsBroken()
        {
            return string.IsNullOrWhiteSpace(_key);
        }

        public string Message => "A request for change must have a key";
    }
}