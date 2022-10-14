using DatabaseLayer.Commands;
using DatabaseLayer.DBContext;
using DatabaseLayer.Queries;
using DatabaseLayer.Utility;
using Model;
using Model.QueryString;
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
    public class UserRolesDal : IUserRolesDal
    {
        private readonly IDBContext _dbContext;


        public UserRolesDal(IDBContext dbContext)
        {
            _dbContext = dbContext;
            if (!BsonClassMap.IsClassMapRegistered(typeof(RoleModel)))
            {
                BsonClassMap.RegisterClassMap<RoleModel>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }

        public async Task<bool> IsExist(string collectionName, string RoleName, string did)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Dictionary<string, object>>(collectionName);
            bool isExist = false;
            BsonDocument filter = new BsonDocument();
            if (!String.IsNullOrEmpty(did))
            {
                filter= new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray().Add(
                        new BsonDocument("Did",new BsonDocument("$ne", did)))
                    .Add(new BsonDocument("RoleName",new BsonDocument("$regex", "^"+RoleName.Trim()+"$").Add("$options", "i")))
                    ) } };
            }
            else
            {
                filter=new BsonDocument { { "$match", new BsonDocument("RoleName", new BsonDocument("$regex", "^" + RoleName.Trim() + "$").Add("$options", "i")) } };
            }
            

            var pipeline = new BsonDocument[] {
                    filter
                };
            var result = await _collection.AggregateAsync<Dictionary<string, object>>(pipeline);
            if (result.FirstOrDefault()!=null)
            {
                isExist = true;
            }
            return isExist;
        }

        public async Task<string> SaveAsync(string collectionName, RoleModel roleModel)
        {
            try
            {
                var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Dictionary<string,object>>(collectionName);

                Dictionary<string, object> dicModel = new Dictionary<string, object>();
                dicModel.Add("Did", roleModel.Did);
                dicModel.Add("RoleName", roleModel.RoleName);
                dicModel.Add("Description", roleModel.Description);
                dicModel.Add("AppId", roleModel.AppId);
                dicModel.Add("CreatedBy", roleModel.CreatedBy);
                dicModel.Add("InsertedDate", roleModel.InsertedDate);
                await _collection.InsertOneAsync(dicModel);
                return roleModel.Did;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> UpdateAsync(string collectionName,  RoleModel roleModel)
        {
            try
            {
                var _rolesCollection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<RoleModel>(collectionName);
                var filter = Builders<RoleModel>.Filter.Eq(s => s.Did, roleModel.Did);

                var update = Builders<RoleModel>.Update
                    .Set("RoleName", roleModel.RoleName)
                    .Set("Description", roleModel.Description)
                    .Set("AppId", roleModel.AppId)
                    .Set("UpdatedBy", roleModel.UpdatedBy)
                    .Set("UpdatedDate", roleModel.UpdatedDate);

                await _rolesCollection.UpdateOneAsync(filter, update);
                return roleModel.Did;
            }
            catch
            {
                throw;
            }
        }

        

        public async Task<IEnumerable<Dictionary<string, object>>> GetRoleAsync(string collectionName, GetAllUserRoles queryModel)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Dictionary<string, object>>(collectionName);

            BsonDocument filter = new BsonDocument();
            if (!String.IsNullOrEmpty(queryModel.QueryModel.AppId) && !String.IsNullOrEmpty(queryModel.QueryModel.RoleName))
            {

                filter = new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray()
                    .Add(new BsonDocument("AppId", new BsonDocument("$regex", queryModel.QueryModel.AppId).Add("$options", "i")))
                    .Add(new BsonDocument("RoleName", new BsonDocument("$regex", queryModel.QueryModel.RoleName).Add("$options", "i")))) } };
            }
            else if (!String.IsNullOrEmpty(queryModel.QueryModel.AppId))
            {

                filter = new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray()
                    .Add(new BsonDocument("AppId", new BsonDocument("$regex", queryModel.QueryModel.AppId).Add("$options", "i")))) } };
            }
            else if (!String.IsNullOrEmpty(queryModel.QueryModel.RoleName))
            {

                filter = new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray()
                    .Add(new BsonDocument("RoleName", new BsonDocument("$regex", queryModel.QueryModel.RoleName).Add("$options", "i")))) } };
            }
            else
            {
                filter=new BsonDocument("$match",new BsonDocument("Did",new BsonDocument("$ne", "")));
            }


            var pipeline = new BsonDocument[] {
                    filter,
                    new BsonDocument{ { "$project", new BsonDocument {
                       {"_id",0}
                    }}},
                    new BsonDocument{ { "$skip", queryModel.Paging.Skip}},
                    new BsonDocument{ { "$limit", queryModel.Paging.Limit}}
                };

            var m = await _collection.AggregateAsync<Dictionary<string, object>>(pipeline);
            var result = await m.ToListAsync();
            return result;

        }


        public async Task<long> GetUserRoleListTotalCount(string collectionName, GetUserRolesListTotalByQuery queryModel)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Dictionary<string, object>>(collectionName);

            BsonDocument filter = new BsonDocument();
            if (!String.IsNullOrEmpty(queryModel.QueryParam.AppId) && !String.IsNullOrEmpty(queryModel.QueryParam.RoleName))
            {

                filter = new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray()
                    .Add(new BsonDocument("AppId", new BsonDocument("$regex", queryModel.QueryParam.AppId).Add("$options", "i")))
                    .Add(new BsonDocument("RoleName", new BsonDocument("$regex", queryModel.QueryParam.RoleName).Add("$options", "i")))) } };
            }
            else if (!String.IsNullOrEmpty(queryModel.QueryParam.AppId))
            {

                filter = new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray()
                    .Add(new BsonDocument("AppId", new BsonDocument("$regex", queryModel.QueryParam.AppId).Add("$options", "i")))) } };
            }
            else if (!String.IsNullOrEmpty(queryModel.QueryParam.RoleName))
            {

                filter = new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray()
                    .Add(new BsonDocument("RoleName", new BsonDocument("$regex", queryModel.QueryParam.RoleName).Add("$options", "i")))) } };
            }
            else
            {
                filter = new BsonDocument("$match", new BsonDocument("Did", new BsonDocument("$ne", "")));
            }


            var pipeline = new BsonDocument[] {
                    filter
                };

            var m = await _collection.AggregateAsync<Dictionary<string, object>>(pipeline);
            var result = await m.ToListAsync();
            return result.Count();
        }

        

        public async Task<Dictionary<string, object>> GetRoleByDidAsync(string collectionName, string did)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<RoleModel>(collectionName);
            var pipeline = new BsonDocument[] {
                    new BsonDocument{ { "$match",  new BsonDocument("$and", new BsonArray().Add(new BsonDocument("Did", did))) } },
                    new BsonDocument{ { "$project", new BsonDocument {
                        {"_id",0}
                    }}},
                };
            var m = await _collection.AggregateAsync<Dictionary<string, object>>(pipeline);
            var result = await m.FirstOrDefaultAsync();
            return result;
        }

    }
}
