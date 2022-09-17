namespace Demkin.Core
{
    public static class ApiResultBuilder
    {
        public static ApiResponse<string> Success()
        {
            var result = new ApiResponse<string>(ExpectResult.Success, "Success", "");
            return result;
        }

        public static ApiResponse<string> Success(string data)
        {
            var result = new ApiResponse<string>(ExpectResult.Success, "Success", data);
            return result;
        }

        public static ApiResponse<string> Fail()
        {
            var result = new ApiResponse<string>(ExpectResult.Fail, "发生错误", "");
            return result;
        }

        public static ApiResponse<string> Fail(string message)
        {
            var result = new ApiResponse<string>(ExpectResult.Fail, message, "");
            return result;
        }
    }

    public static class ApiResultBuilder<T>
    {
        public static ApiResponse<T> Success(T data)
        {
            var result = new ApiResponse<T>(ExpectResult.Success, "Success", data);
            return result;
        }

        public static ApiResponse<T> Fail(string message, T data)
        {
            var result = new ApiResponse<T>(ExpectResult.Fail, message, data);
            return result;
        }
    }
}