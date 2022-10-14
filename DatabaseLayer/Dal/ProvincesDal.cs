using DatabaseLayer.DBContext;
using Model;
using Model.UserSystem;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Dal
{
    public class ProvincesDal
    {
        private readonly IDBContext _dbContext;

        public ProvincesDal(IDBContext dbContext)
        {
            _dbContext = dbContext;
            if (!BsonClassMap.IsClassMapRegistered(typeof(Inventory_Province)))
            {
                BsonClassMap.RegisterClassMap<Inventory_Province>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }

        public async Task<IEnumerable<Dictionary<string, object>>> GetProvince(string collectionName, string province, Pagination paging)
        {
            try
            {
                var _inventoryprovinceCollection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Inventory_Province>(collectionName);

                var pipeline = new BsonDocument[] {
                    new BsonDocument{ { "$match", new BsonDocument("Province", new BsonDocument("$regex", province ).Add("$options", "i")) } },
                    new BsonDocument{ { "$project", new BsonDocument {
                        {"Did","$Did"},
                        {"Province","$Province" }
                    }}},
                    new BsonDocument{ { "$limit", paging.Limit}},
                    new BsonDocument{ { "$skip", paging.Skip}}
                };

                var m = await _inventoryprovinceCollection.AggregateAsync<Dictionary<string, object>>(pipeline);
                var result = await m.ToListAsync();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Dictionary<string, object>> GetProvinceByDidAsync(string collectionName, string did)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Inventory_Province>(collectionName);
            //var filter = Builders<Inventory_Province>.Filter.Eq(s => s.Did, did);

            //var result = await _collection.FindAsync(filter);
            var pipeline = new BsonDocument[] {
                    new BsonDocument{ { "$match",  new BsonDocument("$and", new BsonArray().Add(new BsonDocument("Did", did))) } },
                    new BsonDocument{ { "$project", new BsonDocument {
                        {"_id",0}
                    }}}
                };
            var m = await _collection.AggregateAsync<Dictionary<string, object>>(pipeline);
            var result = await m.FirstOrDefaultAsync();
            return result;
        }
    }
}
