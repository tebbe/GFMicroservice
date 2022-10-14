using DatabaseLayer.DBContext;
using Model;
using Model.QueryString;
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
    public class UserPermissionDal
    {
        private readonly IDBContext _dbContext;

        public UserPermissionDal(IDBContext dbContext)
        {
            _dbContext = dbContext;
            if (!BsonClassMap.IsClassMapRegistered(typeof(UserPermissionModel)))
            {
                BsonClassMap.RegisterClassMap<UserPermissionModel>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }

        public async Task<bool> IsExist(string collectionName, string permissionname, string permissionkey, string did)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Dictionary<string, object>>(collectionName);
            bool isExist = false;
            BsonDocument filter = new BsonDocument();
            if (!String.IsNullOrEmpty(did))
            {
                filter = new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray().Add(
                        new BsonDocument("Did",new BsonDocument("$ne", did)))
                    .Add(new BsonDocument("PermissionName",new BsonDocument("$regex", "^"+ permissionname.Trim()+"$").Add("$options", "i")))
                    .Add(new BsonDocument("PermissionKey",new BsonDocument("$regex", "^"+ permissionkey.Trim()+"$").Add("$options", "i")))
                    ) } };
            }
            else
            {
                filter = new BsonDocument { { "$match", new BsonDocument("$and", new BsonArray()
                    .Add(new BsonDocument("PermissionName", new BsonDocument("$regex", "^" + permissionname.Trim() + "$").Add("$options", "i")))
                    .Add(new BsonDocument("PermissionKey", new BsonDocument("$regex", "^" + permissionkey.Trim() + "$").Add("$options", "i")))
                    ) } };
            }


            var pipeline = new BsonDocument[] {
                    filter
                };
            var result = await _collection.AggregateAsync<Dictionary<string, object>>(pipeline);
            if (result.FirstOrDefault() != null)
            {
                isExist = true;
            }
            return isExist;
        }

        public async Task<string> SaveAsync(string collectionName, UserPermissionModel model)
        {
            try
            {
                var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<UserPermissionModel>(collectionName);

                await _collection.InsertOneAsync(model);
                return model.Did;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> UpdateAsync(string collectionName, UserPermissionModel model)
        {
            try
            {
                var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<UserPermissionModel>(collectionName);
                var filter = Builders<UserPermissionModel>.Filter.Eq(s => s.Did, model.Did);

                var update = Builders<UserPermissionModel>.Update
                    .Set("AppId", model.AppId)
                    .Set("Description", model.Description)
                    .Set("PermissionName", model.PermissionName)
                    .Set("PermissionKey", model.PermissionKey)
                    .Set("UpdatedBy", model.UpdatedBy)
                    .Set("UpdatedDate", model.UpdatedDate);

                await _collection.UpdateOneAsync(filter, update);
                return model.Did;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Dictionary<string, object>> GetByDidAsync(string collectionName, string did)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<UserPermissionModel>(collectionName);
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

        public async Task<IEnumerable<Dictionary<string, object>>> GetAsync(string collectionName, UserPermissionQueryModel queryModel, Pagination paging)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Dictionary<string, object>>(collectionName);

            BsonDocument filter = new BsonDocument();
            if (!String.IsNullOrEmpty(queryModel.AppId) && !String.IsNullOrEmpty(queryModel.PermissionName))
            {

                filter = new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray()
                    .Add(new BsonDocument("AppId", new BsonDocument("$regex", queryModel.AppId).Add("$options", "i")))
                    .Add(new BsonDocument("PermissionName", new BsonDocument("$regex", queryModel.PermissionName).Add("$options", "i")))) } };
            }
            else if (!String.IsNullOrEmpty(queryModel.AppId))
            {

                filter = new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray()
                    .Add(new BsonDocument("AppId", new BsonDocument("$regex", queryModel.AppId).Add("$options", "i")))) } };
            }
            else if (!String.IsNullOrEmpty(queryModel.PermissionName))
            {

                filter = new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray()
                    .Add(new BsonDocument("PermissionName", new BsonDocument("$regex", queryModel.PermissionName).Add("$options", "i")))) } };
            }
            else
            {
                filter = new BsonDocument("$match", new BsonDocument("Did", new BsonDocument("$ne", "")));
            }


            var pipeline = new BsonDocument[] {
                    filter,
                    new BsonDocument{ { "$project", new BsonDocument {
                       {"_id",0}
                    }}},
                    new BsonDocument{ { "$skip", paging.Skip}},
                    new BsonDocument{ { "$limit", paging.Limit}}
                };

            var m = await _collection.AggregateAsync<Dictionary<string, object>>(pipeline);
            var result = await m.ToListAsync();
            return result;

        }

        public async Task<IEnumerable<Dictionary<string, object>>> GetPermissionAsync(string collectionName, UserPermissionQueryModel queryModel, Pagination paging)
        {
            //user system related
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Dictionary<string, object>>(collectionName);

            BsonDocument filter = new BsonDocument();
            if (!String.IsNullOrEmpty(queryModel.AppId))
            {

                filter = new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray()
                    .Add(new BsonDocument("AppId", new BsonDocument("$regex", "^"+queryModel.AppId+"$").Add("$options", "i")))) } };
            }
            else
            {
                filter = new BsonDocument("$match", new BsonDocument());
            }


            var pipeline = new BsonDocument[] {
                    filter,
                    new BsonDocument{ { "$project", new BsonDocument {
                       {"_id",0}
                    }}}
                };

            var m = await _collection.AggregateAsync<Dictionary<string, object>>(pipeline);
            var result = await m.ToListAsync();
            return result;

        }

        public async Task<long> GetTotalCount(string collectionName, UserPermissionQueryModel queryModel)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Dictionary<string, object>>(collectionName);

            BsonDocument filter = new BsonDocument();
            if (!String.IsNullOrEmpty(queryModel.AppId) && !String.IsNullOrEmpty(queryModel.PermissionName))
            {

                filter = new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray()
                    .Add(new BsonDocument("AppId", new BsonDocument("$regex", queryModel.AppId).Add("$options", "i")))
                    .Add(new BsonDocument("PermissionName", new BsonDocument("$regex", queryModel.PermissionName).Add("$options", "i")))) } };
            }
            else if (!String.IsNullOrEmpty(queryModel.AppId))
            {

                filter = new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray()
                    .Add(new BsonDocument("AppId", new BsonDocument("$regex", queryModel.AppId).Add("$options", "i")))) } };
            }
            else if (!String.IsNullOrEmpty(queryModel.PermissionName))
            {

                filter = new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray()
                    .Add(new BsonDocument("PermissionName", new BsonDocument("$regex", queryModel.PermissionName).Add("$options", "i")))) } };
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


    }
}
