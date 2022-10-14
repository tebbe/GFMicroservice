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

namespace DatabaseLayer.Dal
{
    public class UserDepartmentDal
    {
        private readonly IDBContext _dbContext;

        public UserDepartmentDal(IDBContext dbContext)
        {
            _dbContext = dbContext;
            if (!BsonClassMap.IsClassMapRegistered(typeof(UserDepartmentModel)))
            {
                BsonClassMap.RegisterClassMap<UserDepartmentModel>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }

        public async Task<IEnumerable<UserDepartmentModel>> GetAsync(string collectionName, int Skip, int Limit)
        {
            try
            {
                var userDepartmentCollection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<UserDepartmentModel>(collectionName);
                //var obj = await userDepartmentCollection.Find(x => true).Skip(Skip).Limit(Limit).ToListAsync();
                //var result = await userDepartmentCollection.Find(x=> true).Project(x => x.Name).Skip(Skip).Limit(Limit).ToListAsync();
                //var projection = Builders<UserDepartmentModel>.Projection.Include(x => x.Name).Include(x=> x.Description);

                
                //string[] x = customColumnName.Split(",");
                //var projectionList = Builders<UserDepartmentModel>.Projection.Include(x=> x.Did);
                //foreach (var item in x)
                //{
                //    projectionList.Include(item);

                //}
                //return await userDepartmentCollection.Find(x => true)
                //                    .Project<UserDepartmentModel>(projectionList).Skip(Skip).Limit(Limit).ToListAsync();
                return await userDepartmentCollection.Find(x => true).Skip(Skip).Limit(Limit).ToListAsync();
            }
            catch
            {
                throw;
            }

        }

    }
}
