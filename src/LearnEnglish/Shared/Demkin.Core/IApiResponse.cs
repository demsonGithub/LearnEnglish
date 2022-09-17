namespace Demkin.Core
{
    public interface IApiResponse<T>
    {
        ExpectResult code { get; }

        string message { get; }

        T data { get; }
    }

    public enum ExpectResult
    {
        Fail = 0,

        Success = 1
    }
}