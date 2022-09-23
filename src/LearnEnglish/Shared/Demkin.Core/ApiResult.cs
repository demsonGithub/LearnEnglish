namespace Demkin.Core
{
    public class ApiResult<T> : IApiResult<T>
    {
        public ExpectResult code { get; }

        public string message { get; }

        public T data { get; }

        public ApiResult(ExpectResult code, string message, T data)
        {
            this.code = code;
            this.message = message;
            this.data = data;
        }
    }
}