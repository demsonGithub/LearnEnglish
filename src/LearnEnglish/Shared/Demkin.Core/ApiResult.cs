namespace Demkin.Core
{
    public class ApiResult<T>
    {
        public ApiCode code { get; set; } = ApiCode.Success;

        public string msg { get; set; } = "";

        public T data { get; set; }

        public static ApiResult<T> Build()
        {
            return new ApiResult<T>()
            {
                code = ApiCode.Success,
                msg = "success",
            };
        }

        public static ApiResult<T> Build(string msg)
        {
            return new ApiResult<T>()
            {
                code = ApiCode.Success,
                msg = msg,
            };
        }

        public static ApiResult<T> Build(T data)
        {
            return new ApiResult<T>()
            {
                code = ApiCode.Success,
                msg = "success",
                data = data
            };
        }

        public static ApiResult<T> Build(ApiCode code, string msg)
        {
            return new ApiResult<T>()
            {
                code = code,
                msg = msg,
            };
        }

        public static ApiResult<T> Build(ApiCode code, string msg, T data)
        {
            return new ApiResult<T>()
            {
                code = code,
                msg = msg,
                data = data
            };
        }
    }
}