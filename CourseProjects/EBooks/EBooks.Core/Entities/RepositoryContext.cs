namespace EBooks.Core.Entities;

public class RepositoryContext<DbModel>
{
    public List<DbModel> Data { get; set; }
    public uint Counter { get; set; }
}