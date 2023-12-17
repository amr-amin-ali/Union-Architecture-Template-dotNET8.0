namespace Elearning.Services.Common
{
    using Elearning.Contracts.Common;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly ILogger? _logger;
        protected string? ControllerName => ControllerContext.RouteData.Values["controller"]!.ToString();
        protected string? ActionName => ControllerContext.RouteData.Values["action"]!.ToString();
        protected string? ClientRemoteIP => Request.Headers.ContainsKey("X-Forwarded-For")
            ? Request.Headers["X-Forwarded-For"].ToString()
            : HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
        protected static string? AssemblyName => System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        public BaseController(ILogger logger)
        {
            _logger = logger;
        }

        public BaseController() { }

        public ActionResult ReturnResult<T>(Response<T> operationResult)
        {
            try
            {
                if (!operationResult.IsSuccessful)
                {
                    return StatusCode(operationResult.Error.ErrorCode, operationResult);
                }

                return Ok(operationResult);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
