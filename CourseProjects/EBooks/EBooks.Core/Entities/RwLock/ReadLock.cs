namespace EBooks.Core.Entities.RwLock;

public class ReadLock : IDisposable
{
    private readonly ReaderWriterLockSlim _lock;
    public ReadLock(ReaderWriterLockSlim @lock)
    {
        _lock = @lock;
        _lock.EnterReadLock();
    }
    public void Dispose() => _lock.ExitReadLock();
}