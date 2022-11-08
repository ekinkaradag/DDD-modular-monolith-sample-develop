using TestApp.BuildingBlocks.Domain;

namespace TestApp.Module.Rfc.Domain.RequestForChange.Events
{
    internal class RequestForChangeWithdrawnEvent : IDomainEvent
    {
        public string RfcKey { get; }

        public RequestForChangeWithdrawnEvent(string rfcKey)
        {
            RfcKey = rfcKey;
        }
    }
}