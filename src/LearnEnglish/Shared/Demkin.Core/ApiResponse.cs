namespace Demkin.Core
{
    public class ApiResponse<T> : IApiResponse<T>
    {
        public ExpectResult code { get; }

        public string message { get; }

        public T data { get; }

        public ApiResponse(ExpectResult code, string message, T data)
        {
            this.code = code;
            this.message = message;
            this.data = data;
        }
    }
}