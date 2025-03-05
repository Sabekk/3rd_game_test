using ObjectPooling;

public interface IPoolable
{
    public PoolObject Poolable { get; }
    public void AssignPoolable(PoolObject poolable);
}
