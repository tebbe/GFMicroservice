using DatabaseLayer.DBContext;
using Model;
using MongoDB.Bson;
using System.Text.Json;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseLayer.Utility;
using Model.QueryString;

namespace DatabaseLayer.Dal
{
    public class AlertsDal
    {
        private readonly IDBContext _dbContext;

        public AlertsDal(IDBContext dbContext)
        {
            _dbContext = dbContext;
            if (!BsonClassMap.IsClassMapRegistered(typeof(AlertModel)))
            {
                BsonClassMap.RegisterClassMap<AlertModel>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }

        public async Task<IEnumerable<AlertModel>> GetAsync(string collectionName, AlertQueryModel queryparam, Pagination paging)
        {
            try
            {
                var _osAlertCollection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<AlertModel>(collectionName);

                var filterBuilder = Builders<AlertModel>.Filter;
                var filter = filterBuilder.In(m => m.BuildingID, queryparam.BuildingIds); ;

                if (!String.IsNullOrEmpty(queryparam.Floorid))
                {
                    filter = filter & filterBuilder.Eq(m => m.FloorID, queryparam.Floorid);
                }
                if (!String.IsNullOrEmpty(queryparam.Resolved))
                {
                    filter = filter & filterBuilder.Eq(m => m.Resolved, queryparam.Resolved);
                }
                if (!String.IsNullOrEmpty(queryparam.Severity))
                {
                    filter = filter & filterBuilder.Eq(m => m.Severity, queryparam.Severity);
                }
                
                var result = await _osAlertCollection.Find(filter).Skip(paging.Skip).Limit(paging.Limit).ToListAsync();
                return result;

            }
            catch
            {
                throw;
            }

        }

        public async Task<long> GetTotalCountAsync(string collectionName, AlertQueryModel queryparam)
        {
            try
            {
                var _osAlertCollection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<AlertModel>(collectionName);
                var filterBuilder = Builders<AlertModel>.Filter;
                var filter = filterBuilder.In(m => m.BuildingID, queryparam.BuildingIds); ;

                if (!String.IsNullOrEmpty(queryparam.Floorid))
                {
                    filter = filter & filterBuilder.Eq(m => m.FloorID, queryparam.Floorid);
                }
                if (!String.IsNullOrEmpty(queryparam.Resolved))
                {
                    filter = filter & filterBuilder.Eq(m => m.Resolved, queryparam.Resolved);
                }
                if (!String.IsNullOrEmpty(queryparam.Severity))
                {
                    filter = filter & filterBuilder.Eq(m => m.Severity, queryparam.Severity);
                }
                var result = await _osAlertCollection.Find(filter).CountDocumentsAsync();
                return result;

            }
            catch
            {
                throw;
            }

        }
    }
}
