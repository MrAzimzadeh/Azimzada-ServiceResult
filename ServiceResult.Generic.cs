using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Azimzada.ServiceResult
{
    /// <summary>
    /// Represents a service result with data.
    /// </summary>
    public class ServiceResult<T> : IActionResult
    {
        private readonly int _statusCode;
        private readonly T _data;
        private readonly string _errorCode;
        private readonly bool _isFailedResult;

        public T Data => _data;
        public int StatusCode => _statusCode;
        public string ErrorCode => _errorCode;
        public bool IsFailedResult => _isFailedResult;

        protected ServiceResult(int statusCode)
        {
            _statusCode = statusCode;
            _data = default;
            _errorCode = null;
            _isFailedResult = false;
        }

        protected ServiceResult(int statusCode, T data, bool isFailedResult)
        {
            _statusCode = statusCode;
            _data = data;
            _errorCode = null;
            _isFailedResult = isFailedResult;
        }

        protected ServiceResult(int statusCode, string errorCode)
        {
            _statusCode = statusCode;
            _data = default;
            _errorCode = errorCode;
            _isFailedResult = true;
        }

        public static ServiceResult<T> Success(int statusCode)
        {
            return new ServiceResult<T>(statusCode);
        }

        public static ServiceResult<T> Ok()
        {
            return new ServiceResult<T>(200);
        }

        public static ServiceResult<T> Ok(T data)
        {
            return new ServiceResult<T>(200, data, false);
        }

        public static ServiceResult<T> Created(T data = default)
        {
            return new ServiceResult<T>(201, data, false);
        }

        public static ServiceResult<T> Success(int statusCode, T data)
        {
            return new ServiceResult<T>(statusCode, data, false);
        }

        public static ServiceResult<T> Fail(int statusCode, string errorCode)
        {
            return new ServiceResult<T>(statusCode, errorCode);
        }

        public static ServiceResult<T> Fail(int statusCode, T data)
        {
            return new ServiceResult<T>(statusCode, data, true);
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            if (_data is IActionResult actionResult)
            {
                await actionResult.ExecuteResultAsync(context);
            }
            else
            {
                var objectResult = new ObjectResult(_data ?? (object)_errorCode)
                {
                    StatusCode = _statusCode,
                };
                await objectResult.ExecuteResultAsync(context);
            }
        }
    }
}
