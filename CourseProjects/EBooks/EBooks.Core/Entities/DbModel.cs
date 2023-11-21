namespace EBooks.Core.Entities;

public abstract class DbModel
{
    public uint Id { get; set; }
    public DateTimeOffset AddDate { get; set; }
}