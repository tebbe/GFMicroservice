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
    public class BuildingsDal
    {
        private readonly IDBContext _dbContext;

        public BuildingsDal(IDBContext dbContext)
        {
            _dbContext = dbContext;
            if (!BsonClassMap.IsClassMapRegistered(typeof(Inventory_Building)))
            {
                BsonClassMap.RegisterClassMap<Inventory_Building>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }

        public async Task<IEnumerable<Dictionary<string, object>>> GetActiveBuilding(string collectionName, Pagination paging)
        {
            try
            {
                var _inventorybuildingCollection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Inventory_Building>(collectionName);
                var pipeline = new BsonDocument[] {
                    new BsonDocument{ { "$match",  new BsonDocument("$and", new BsonArray().Add(new BsonDocument("Active", "1"))) } },
                    new BsonDocument{ { "$project", new BsonDocument {
                        {"_id",0}
                    }}},
                    new BsonDocument{ { "$limit", paging.Limit}},
                    new BsonDocument{ { "$skip", paging.Skip}}
                };
                var m = await _inventorybuildingCollection.AggregateAsync<Dictionary<string, object>>(pipeline);
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
