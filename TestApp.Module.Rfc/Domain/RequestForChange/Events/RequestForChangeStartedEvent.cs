using TestApp.BuildingBlocks.Domain;

namespace TestApp.Module.Rfc.Domain.RequestForChange.Events
{
    internal class RequestForChangeStartedEvent : IDomainEvent
    {
        public string RfcKey { get; }

        public RequestForChangeStartedEvent(string rfcKey)
        {
            RfcKey = rfcKey;
        }
    }
}