using DatabaseLayer.DBContext;
using Model;
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
    public class TeamsDal
    {
        private readonly IDBContext _dbContext;

        public TeamsDal(IDBContext dbContext)
        {
            _dbContext = dbContext;
            if (!BsonClassMap.IsClassMapRegistered(typeof(GFTeamsModel)))
            {
                BsonClassMap.RegisterClassMap<GFTeamsModel>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }

        public async Task<IEnumerable<Dictionary<string, object>>> GetAsync(string collectionName)
        {
            try
            {
                var Collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<GFTeamsModel>(collectionName);
                //return await Collection.Find(x => true).ToListAsync();
                var pipeline = new BsonDocument[] {new BsonDocument{ { "$project", new BsonDocument {
                        {"_id",0}
                    }}}
                };

                var m = await Collection.AggregateAsync<Dictionary<string, object>>(pipeline);
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
