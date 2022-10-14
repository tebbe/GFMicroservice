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
    public class EODRunningDal
    {
        private readonly IDBContext _dbContext;
        public EODRunningDal(IDBContext dbContext)
        {
            _dbContext = dbContext;
            if (!BsonClassMap.IsClassMapRegistered(typeof(EODRunning)))
            {
                BsonClassMap.RegisterClassMap<EODRunning>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }

        public async Task<EODRunning> GetEODAsync(string collectionName, string buildingId,string currentMonthDate)
        {
            try
            {
                var _layerCollection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<EODRunning>(collectionName);
                var pipeline = new BsonDocument[] {new BsonDocument{ { "$match", new BsonDocument("$and",new BsonArray { new BsonDocument("Building", buildingId), new BsonDocument("CurrentMonthDate", currentMonthDate) })}}};
                var result = await _layerCollection.AggregateAsync<EODRunning>(pipeline);
                return await result.FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }


    }
}
