namespace EBooks.Core.Entities.RwLock;

public class WriteLock : IDisposable
{
    private readonly ReaderWriterLockSlim _lock;
    public WriteLock(ReaderWriterLockSlim @lock)
    {
        _lock = @lock;
        _lock.EnterWriteLock();
    }
    public void Dispose() => _lock.ExitWriteLock();
}