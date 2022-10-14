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
    public class UserSystemDal
    {
        private readonly IDBContext _dbContext;
        public UserSystemDal(IDBContext dbContext)
        {
            _dbContext = dbContext;
            if (!BsonClassMap.IsClassMapRegistered(typeof(UserSystemModel)))
            {
                BsonClassMap.RegisterClassMap<UserSystemModel>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }
        public async Task<UserSystemModel> GetAsync(string collectionName, string did)
        {
            try
            {
                var _premiseAppSettingsCollection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<UserSystemModel>(collectionName);
                var result = await _premiseAppSettingsCollection.FindAsync(x => x.Did == did);
                return await result.FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
