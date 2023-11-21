using System.Text.Json;
using EBooks.Core.Entities;
using EBooks.Core.Entities.RwLock;
using EBooks.Core.Extensions;
using Flow;
using Flow.StandardOperationError;

namespace EBooks.DA.Repositories;

public class FileRepository<TDbModel> where TDbModel : DbModel
{
    private readonly RWLock _rwLock = new();
    private readonly string _fileName = $"{typeof(TDbModel).FullName}.json";

    public List<TDbModel> GetAll() 
    {
        return Read().Data;
    }
    
    public Operation<TDbModel> GetById(uint id) 
    {
        var data =  Read().Data;
        var dbModelIndex = data.BinarySearch(id);
        if(dbModelIndex >= 0)
        {
            return data[dbModelIndex];
        }
        
        return Operation<TDbModel>.Failed(Errors.BookNotFoundOperationError);
    }

    public TDbModel Upsert(TDbModel dbModel) {

        Save((context) =>
        {
            var dbModelIndex = ListExtensions.BinarySearch(context.Data, dbModel);
            if(dbModelIndex >= 0)
            {
                context.Data[dbModelIndex] = dbModel;
            }
            else
            {
                dbModel.Id = context.Counter++;
                // todo add data
                // dbModel.
                context.Data.Add(dbModel);
            }
        });
        return dbModel;
    }

    public List<TDbModel> Delete(uint id) {

        return Save((context) =>
        {
            var dbModelIndex = context.Data.BinarySearch(id);
            if(dbModelIndex >= 0)
            {
                context.Data.RemoveAt(dbModelIndex);
            }
        });
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

    private RepositoryContext<TDbModel> Read()
    {
        using (_rwLock.ReadLock())
        {
            if (!File.Exists(_fileName))
                return new RepositoryContext<TDbModel>();
            
            return JsonSerializer.Deserialize<RepositoryContext<TDbModel>>(
                File.ReadAllText(_fileName)) ?? new RepositoryContext<TDbModel>();
        }
    }
}