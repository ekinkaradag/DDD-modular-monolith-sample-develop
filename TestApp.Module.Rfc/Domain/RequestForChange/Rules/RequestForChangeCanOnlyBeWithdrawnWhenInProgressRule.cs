using TestApp.BuildingBlocks.Domain;

namespace TestApp.Module.Rfc.Domain.RequestForChange.Rules
{
    internal class RequestForChangeCanOnlyBeWithdrawnWhenInProgressRule : IBusinessRule
    {
        private readonly string _currentStatus;

        public RequestForChangeCanOnlyBeWithdrawnWhenInProgressRule(string currentStatus)
        {
            _currentStatus = currentStatus;
        }

        public bool IsBroken()
        {
            return _currentStatus != "INPROGRESS";
        }

        public string Message => "Request for change can only be withdrawn when in progress";
    }
}