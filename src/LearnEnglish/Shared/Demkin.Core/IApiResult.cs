namespace Demkin.Core
{
    public interface IApiResult<T>
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