using Microsoft.AspNetCore.Mvc;
using TestApp.BuildingBlocks.Application;

namespace TestApp.Api.Validation
{
    public class NotFoundExceptionProblemDetails : ProblemDetails
    {
        public NotFoundExceptionProblemDetails(NotFoundException exception)
        {
            Title = "Resource does not exist";
            Status = StatusCodes.Status404NotFound;
            Detail = exception.Message;
            Type = "https://somedomain/resource-not-found-error";
        }
    }
}