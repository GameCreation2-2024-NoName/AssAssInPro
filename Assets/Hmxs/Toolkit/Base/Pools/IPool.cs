namespace Hmxs.Toolkit.Base.Pools
{
    public interface IPool<T> where T : class
    {
        T Get();
        void Release(T element);
        void Dispose();
    }
}