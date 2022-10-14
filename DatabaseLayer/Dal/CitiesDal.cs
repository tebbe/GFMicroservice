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
    public class CitiesDal
    {

        private readonly IDBContext _dbContext;

        public CitiesDal(IDBContext dbContext)
        {
            _dbContext = dbContext;
            if (!BsonClassMap.IsClassMapRegistered(typeof(Inventory_City)))
            {
                BsonClassMap.RegisterClassMap<Inventory_City>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }

        public async Task<Dictionary<string,object>> GetCityByDidAsync(string collectionName, string did)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Inventory_City>(collectionName);
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

        public async Task<IEnumerable<Dictionary<string, object>>> GetCity(string collectionName, string provinceid, string city, Pagination paging)
        {
            try
            {
                var _inventorycityCollection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Inventory_City>(collectionName);

                var pipeline = new BsonDocument[] {
                    new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray().Add(new BsonDocument("ProvinceID", provinceid))
                    .Add(new BsonDocument("$or", new BsonArray().Add(new BsonDocument("City", new BsonDocument("$regex", city ).Add("$options", "i")))))) } },
                    new BsonDocument{ { "$project", new BsonDocument {
                        {"Did","$Did"},
                        {"City","$City" }
                    }}},
                    new BsonDocument{ { "$limit", paging.Limit}},
                    new BsonDocument{ { "$skip", paging.Skip}}
                };


                var m = await _inventorycityCollection.AggregateAsync<Dictionary<string, object>>(pipeline);
                var result = await m.ToListAsync();
                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}
