using DatabaseLayer.DBContext;
using Microsoft.Extensions.Options;
using Model;
using Model.DBModel.MongoModel;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Dal
{
    public class AppSettingsDal
    {
        private readonly IDBContext _dbContext;
        public AppSettingsDal(IDBContext dbContext)
        {
            _dbContext = dbContext;
            if (!BsonClassMap.IsClassMapRegistered(typeof(PremiseAppSettingsModel)))
            {
                BsonClassMap.RegisterClassMap<PremiseAppSettingsModel>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }
        public async Task<PremiseAppSettingsModel> GetAsync(string collectionName,string appID, string settingKey)
        {
            try
            {
                var _premiseAppSettingsCollection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<PremiseAppSettingsModel>(collectionName);
                var result = await _premiseAppSettingsCollection.FindAsync(x => x.AppId == appID && x.SettingKey == settingKey);
                return await result.FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

        }
    }
}
