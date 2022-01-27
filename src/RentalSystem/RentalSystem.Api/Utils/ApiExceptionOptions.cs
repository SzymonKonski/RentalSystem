using System;
using Microsoft.AspNetCore.Http;

namespace RentalSystem.Api.Utils
{
    public class ApiExceptionOptions
    {
        public Action<HttpContext, Exception, ApiError> AddResponseDetails { get; set; }
    }
}
