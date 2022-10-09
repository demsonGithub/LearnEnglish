namespace Demkin.Core
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICache
    {
        bool SetCache<T>(string key, T value, double? expireTimeSecond = null);

        T GetCache<T>(string key);

        bool RemoveCache(string key);
    }
}