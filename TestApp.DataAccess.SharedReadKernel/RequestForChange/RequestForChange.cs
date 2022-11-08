using System;

namespace IoCore.SharedReadKernel.RequestForChange
{
    public class RequestForChange
    {
        public RequestForChange(string key, string title, DateTime dateRaised, string status)
        {
            Key = key;
            Title = title;
            DateRaised = dateRaised;
            Status = status;
        }

        public Guid Id { get; private set; }

        public string Key { get; private set; }
        
        public string Title { get; private set; }
        
        public DateTime DateRaised { get; private set; }
        
        public string Status { get; private set; }
    }
}