using DatabaseLayer.DBContext;
using Model;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Dal
{
    public class ResourcesFlatDal
    {
        private readonly IDBContext _dbContext;

        public ResourcesFlatDal(IDBContext dbContext)
        {
            _dbContext = dbContext;
            if (!BsonClassMap.IsClassMapRegistered(typeof(GFResourcesFlatModel)))
            {
                BsonClassMap.RegisterClassMap<GFResourcesFlatModel>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }

        public async Task<IEnumerable<GFResourcesFlatModel>> GetAsync(string collectionName, string floorid)
        {
            try
            {
                var Collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<GFResourcesFlatModel>(collectionName);

                if (!string.IsNullOrEmpty(floorid))
                {
                    var result = Collection.Find(x => x.FloorId == floorid).ToListAsync();
                    return await result;
                }
                else
                {
                    var result = Collection.Find(x => true).ToListAsync();
                    return await result;
                }
                
            }
            catch
            {
                throw;
            }

        }

    }
}
