using TestApp.BuildingBlocks.Domain;

namespace TestApp.Module.Rfc.Domain.RequestForChange.Rules
{
    internal class RequestForChangeCanOnlyBeCompletedWhenInProgressRule : IBusinessRule
    {
        private readonly string _currentStatus;

        public RequestForChangeCanOnlyBeCompletedWhenInProgressRule(string currentStatus)
        {
            _currentStatus = currentStatus;
        }

        public bool IsBroken()
        {
            return _currentStatus != "INPROGRESS";
        }

        public string Message => "Request for change can only be completed when in progress";
    }
}