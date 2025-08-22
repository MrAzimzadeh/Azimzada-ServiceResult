using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Azimzada.ServiceResult
{
    /// <summary>
    /// Represents a service result with no data.
    /// </summary>
    public class ServiceResult : IActionResult
    {
        private readonly int _statusCode;
        private readonly string _errorCode;
        private readonly bool _isFailedResult;

        public int StatusCode => _statusCode;
        public string ErrorCode => _errorCode;
        public bool IsFailedResult => _isFailedResult;

        protected ServiceResult(int statusCode)
        {
            _statusCode = statusCode;
            _errorCode = null;
            _isFailedResult = false;
        }

        protected ServiceResult(int statusCode, string errorCode)
        {
            _statusCode = statusCode;
            _errorCode = errorCode;
            _isFailedResult = true;
        }

        public static ServiceResult Success(int statusCode = 200)
        {
            return new ServiceResult(statusCode);
        }

        public static ServiceResult Ok()
        {
            return new ServiceResult(200);
        }

        public static ServiceResult Created()
        {
            return new ServiceResult(201);
        }

        public static ServiceResult Fail(int statusCode, string errorCode)
        {
            return new ServiceResult(statusCode, errorCode);
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_errorCode)
            {
                StatusCode = _statusCode,
            };
            await objectResult.ExecuteResultAsync(context);
        }
    }
}
