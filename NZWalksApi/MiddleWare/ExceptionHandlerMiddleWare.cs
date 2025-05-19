using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;

namespace NZWalksApi.MiddleWare
{
    public class ExceptionHandlerMiddleWare
    {
        public readonly ILogger<ExceptionHandlerMiddleWare> _logger;
        public readonly RequestDelegate _requestDelegate;
        public ExceptionHandlerMiddleWare(ILogger<ExceptionHandlerMiddleWare> logger,
            RequestDelegate requestDelegate)
        {
            this._logger = logger;
            this._requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync( HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                //Log  this exception
                _logger.LogError(ex,$"{errorId}:{ex.Message}");

                //Return a custom error response
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id=errorId,
                    Error = "Algo ocurrio! Favor de revisar."
                };
                await httpContext.Response.WriteAsJsonAsync(error);
            }

        }
    }
}
