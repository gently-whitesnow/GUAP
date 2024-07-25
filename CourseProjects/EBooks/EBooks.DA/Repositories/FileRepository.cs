using System.Text.Json;
using EBooks.Core.Entities;
using EBooks.Core.Entities.RwLock;
using EBooks.Core.Extensions;
using Flow;

namespace EBooks.DA.Repositories;

public class FileRepository<TDbModel> where TDbModel : DbModel
{
    private readonly RWLock _rwLock = new();
    private readonly string _fileName = $"{typeof(TDbModel).FullName}.json";

    public List<TDbModel> GetAll()
    {
        return ReadSave().Data;
    }

    public Result<TDbModel> GetById(uint id)
    {
        var data = ReadSave().Data;
        var dbModelIndex = data.BinarySearch(id);
        if (dbModelIndex >= 0)
        {
            return data[dbModelIndex];
        }

        return Errors.BookNotFoundError;
    }

    public TDbModel Insert(TDbModel dbModel)
    {
        Save((context) =>
        {
            dbModel.Id = context.Counter++;
            dbModel.AddDate = DateTimeOffset.UtcNow;
            context.Data.Add(dbModel);
        });
        return dbModel;
    }
    
    public Result<TDbModel> Update(TDbModel dbModel)
    {
        Result<TDbModel>? operation = null;
        Save((context) =>
        {
            var dbModelIndex = ListExtensions.BinarySearch(context.Data, dbModel);
            if (dbModelIndex >= 0)
            {
                context.Data[dbModelIndex] = dbModel;
                return;
            }
            
            operation = Errors.BookNotFoundError;
        });
        return operation ?? dbModel;
    }

    public Result Delete(uint id)
    {
        Result? operation = null;
        Save((context) =>
        {
            var dbModelIndex = context.Data.BinarySearch(id);
            if (dbModelIndex >= 0)
            {
                context.Data.RemoveAt(dbModelIndex);
            }

            operation = Errors.BookNotFoundError;
        });
        return operation ?? Result.Success;
    }

    private List<TDbModel> Save(Action<RepositoryContext<TDbModel>> modificationAction)
    {
        using (_rwLock.WriteLock())
        {
            var context = Read();
            modificationAction(context);
            File.WriteAllText(_fileName, JsonSerializer.Serialize(context));
            return context.Data;
        }
    }

    private RepositoryContext<TDbModel> ReadSave()
    {
        using (_rwLock.ReadLock())
        {
            return Read();
        }
    }

    private RepositoryContext<TDbModel> Read()
    {
        if (!File.Exists(_fileName))
            return new RepositoryContext<TDbModel>
            {
                Counter = 0,
                Data = new List<TDbModel>()
            };

        return JsonSerializer.Deserialize<RepositoryContext<TDbModel>>(
            File.ReadAllText(_fileName)) ?? new RepositoryContext<TDbModel>();
    }
}