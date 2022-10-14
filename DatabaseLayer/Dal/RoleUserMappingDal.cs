using DatabaseLayer.DBContext;
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
    public class RoleUserMappingDal
    {
        private readonly IDBContext _dbContext;

        public RoleUserMappingDal(IDBContext dbContext)
        {
            _dbContext = dbContext;
            if (!BsonClassMap.IsClassMapRegistered(typeof(RoleUserMapping)))
            {
                BsonClassMap.RegisterClassMap<RoleUserMapping>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }

        public async Task<bool> SaveAsync(string collectionName, RoleUserMapping model)
        {
            try
            {
                var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<RoleUserMapping>(collectionName);
                string[] temp = model.RoleID.Split(',');
                foreach (string item in temp)
                {
                    RoleUserMapping data = new RoleUserMapping() 
                    {
                        Did = Guid.NewGuid().ToString("N"), 
                        RoleID = item, 
                        UserID = model.UserID,
                        InsertedDate = model.InsertedDate,
                        UpdatedDate = model.UpdatedDate,
                        CreatedBy = model.CreatedBy,
                        UpdatedBy = model.UpdatedBy
                    };
                    await _collection.InsertOneAsync(data);
                    
                }
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteAsync(string collectionName, string userid)
        {
            try
            {
                var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<RoleUserMapping>(collectionName);
                await _collection.DeleteManyAsync(m => m.UserID == userid);
                return true;
            }
            catch
            {
                throw;
            }
        }

    }
}
