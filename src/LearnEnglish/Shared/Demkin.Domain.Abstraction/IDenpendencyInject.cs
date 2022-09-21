namespace Demkin.Domain.Abstraction
{
    /// <summary>
    /// 标记Autofac注入,单例Singleton
    /// </summary>
    public interface IDenpendencySingleton
    { }

    /// <summary>
    /// 标记Autofac注入,范围Scope
    /// </summary>
    public interface IDenpendencyScope
    { }

    /// <summary>
    /// 标记Autofac注入,瞬时Transient
    /// </summary>
    public interface IDenpendencyTransient
    { }
}