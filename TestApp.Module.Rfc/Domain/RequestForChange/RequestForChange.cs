using System;
using TestApp.BuildingBlocks.Domain;
using TestApp.Module.Rfc.Domain.RequestForChange.Events;
using TestApp.Module.Rfc.Domain.RequestForChange.Rules;

namespace TestApp.Module.Rfc.Domain.RequestForChange
{
    internal class RequestForChange : Entity, IAggregateRoot
    {
        private const string WithDrawn = "WITHDRAWN";
        private const string Draft = "DRAFT";
        private const string InProgress = "INPROGRESS";

        public string Key { get; }
        public string Title { get; }
        public DateTime DateRaised { get; }
        public string Status { get; private set; }

        private RequestForChange(
            string key,
            string title,
            DateTime dateRaised)
        {
            Key = key;
            Title = title;
            DateRaised = dateRaised;
            Status = Draft;

        }

        public static RequestForChange Create(string key, string title)
        {
            CheckRule(new RequestForChangeRequiresKeyRule(key));
            CheckRule(new RequestForChangeRequiresTitleRule(title));
        
            var requestForChange = new RequestForChange
            (
                key,
                title,
                dateRaised: DateTime.UtcNow
            );

            return requestForChange;
        }

        public void Start()
        {
            CheckRule(new RequestForChangeCanOnlyBeStartedWhenInDraftRule(Status));
        
            Status = InProgress;
        
            AddDomainEvent(new RequestForChangeWithdrawnEvent(Key));
        }

        public void WithDraw()
        {
            CheckRule(new RequestForChangeCanOnlyBeWithdrawnWhenInProgressRule(Status));
        
            Status = WithDrawn;
        
            AddDomainEvent(new RequestForChangeWithdrawnEvent(Key));
        }
    }
}