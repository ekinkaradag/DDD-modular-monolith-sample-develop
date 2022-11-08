using TestApp.BuildingBlocks.Domain;

namespace TestApp.Module.Rfc.Domain.RequestForChange.Rules
{
    internal class RequestForChangeCanOnlyBeStartedWhenInDraftRule : IBusinessRule
    {
        private readonly string _currentStatus;

        public RequestForChangeCanOnlyBeStartedWhenInDraftRule(string currentStatus)
        {
            _currentStatus = currentStatus;
        }

        public bool IsBroken()
        {
            return _currentStatus != "DRAFT";
        }

        public string Message => "Request for change can only be started when in draft";
    }
}