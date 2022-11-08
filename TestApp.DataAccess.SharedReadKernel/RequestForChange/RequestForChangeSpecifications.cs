using System;
using System.Linq;

namespace IoCore.SharedReadKernel.RequestForChange
{
    internal static class RequestForChangeSpecifications
    {
        public static IQueryable<T> That<T>(this IQueryable<T> queryable) where  T : class
        {
            return queryable;
        }

        public static IQueryable<RequestForChange>AreInProgress(this IQueryable<RequestForChange> requestsForChange)
        {
            return requestsForChange.Where(requestForChange => requestForChange.Status == "INPROGRESS");
        }
    
        public static IQueryable<RequestForChange>AreAgainstDeprecatedProducts(this IQueryable<RequestForChange> requestsForChange, DateTime since)
        {
            return requestsForChange.Where(requestForChange => requestForChange.DateRaised > since);
        }
    
        public static IQueryable<RequestForChange>AreWithDrawn(this IQueryable<RequestForChange> requestsForChange)
        {
            return requestsForChange.Where(requestForChange => requestForChange.Status == "WITHDRAWN");
        }
    }
}