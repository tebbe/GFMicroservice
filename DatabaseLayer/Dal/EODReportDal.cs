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
    public class EODReportDal
    {
        private readonly IDBContext _dbContext;
        public EODReportDal(IDBContext dbContext)
        {
            _dbContext = dbContext;
            if (!BsonClassMap.IsClassMapRegistered(typeof(EODReport)))
            {
                BsonClassMap.RegisterClassMap<EODReport>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }

        public async Task<IEnumerable<EODReport>> GetEODReportAsync(string collectionName, string buildingId,string type,string[] others)
        {
            try
            {
                var _layerCollection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<EODReport>(collectionName);

                var pipeline = new BsonDocument[] {
                   new BsonDocument{ { "$match", new BsonDocument("$and",new BsonArray {
                       new BsonDocument("Building", buildingId),new BsonDocument("Type", type),
                       new BsonDocument("Other",new BsonDocument("$in", new BsonArray (others)))
                       })} }
                };

                var result = await _layerCollection.AggregateAsync<EODReport>(pipeline);
                return await result.ToListAsync();
            }
            catch
            {
                throw;
            }
        }


    }
}
