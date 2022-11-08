using TestApp.BuildingBlocks.Domain;

namespace TestApp.Module.Rfc.Domain.RequestForChange.Rules
{
    internal class RequestForChangeRequiresTitleRule : IBusinessRule
    {
        private readonly string _title;

        public RequestForChangeRequiresTitleRule(string title)
        {
            _title = title;
        }

        public bool IsBroken()
        {
            return string.IsNullOrWhiteSpace(_title);
        }

        public string Message => "A request for change must have a title";
    }
}