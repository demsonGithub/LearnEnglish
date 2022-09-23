using System;

namespace Demkin.Core
{
    public static class ApiResultBuilder
    {
        public static ApiResult<string> Success()
        {
            var result = new ApiResult<string>(ExpectResult.Success, "Success", "");
            return result;
        }

        public static ApiResult<string> Success(string data)
        {
            var result = new ApiResult<string>(ExpectResult.Success, "Success", data);
            return result;
        }

        public static ApiResult<string> Fail()
        {
            var result = new ApiResult<string>(ExpectResult.Fail, "发生错误", "");
            return result;
        }

        public static ApiResult<string> Fail(string message)
        {
            var result = new ApiResult<string>(ExpectResult.Fail, message, "");
            return result;
        }
    }

    public static class ApiResultBuilder<T>
    {
        public static ApiResult<T> Success(T data)
        {
            var result = new ApiResult<T>(ExpectResult.Success, "Success", data);
            return result;
        }

        public static ApiResult<T> Fail(string message, T data = default(T))
        {
            var result = new ApiResult<T>(ExpectResult.Fail, message, data);
            return result;
        }
    }
}