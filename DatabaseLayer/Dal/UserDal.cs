using DatabaseLayer.Commands;
using DatabaseLayer.DBContext;
using DatabaseLayer.Queries;
using DatabaseLayer.Utility;
using Model;
using Model.UserSystem;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Dal
{
    public class UserDal : IUserDal
    {
        private readonly IDBContext _dbContext;

        public UserDal(IDBContext dbContext)
        {
            _dbContext = dbContext;
            if (!BsonClassMap.IsClassMapRegistered(typeof(UserModel)))
            {
                BsonClassMap.RegisterClassMap<UserModel>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }

        public async Task<Dictionary<string, object>> GetByIdAsync(string collectionName, string did)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<UserModel>(collectionName);

            var pipeline = new BsonDocument[] {
                    new BsonDocument{ { "$match",  new BsonDocument("$and", new BsonArray().Add(new BsonDocument("Did", did))) } },
                    new BsonDocument{ { "$project", new BsonDocument {
                        {"_id",0},{"Password",0}
                    }}}
                };
            var m = await _collection.AggregateAsync<Dictionary<string, object>>(pipeline);

            var result = await m.FirstOrDefaultAsync();



            return result;
        }

        public async Task<string> SaveAsync(string collectionName, UserModel model)
        {
            try
            {

                var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Dictionary<string, object>>(collectionName);
                Dictionary<string, object> dicModel = new Dictionary<string, object>();
                dicModel.Add("Did", model.Did);
                dicModel.Add("Email", model.Email);
                dicModel.Add("IsVendor", model.IsVendor);
                dicModel.Add("User Name", model.UserName);
                dicModel.Add("Title", model.Title);
                dicModel.Add("First Name", model.FirstName);
                dicModel.Add("Last Name", model.LastName);
                dicModel.Add("PropertyBuilding", model.PropertyBuilding);
                dicModel.Add("PhoneWork", model.PhoneWork);
                dicModel.Add("PhoneMobile", model.PhoneMobile);
                dicModel.Add("EmployeeID", model.EmployeeID);
                dicModel.Add("Rate", model.Rate);
                dicModel.Add("IsCompanyOwner", model.IsCompanyOwner);
                dicModel.Add("AutoLoginEnable", model.AutoLoginEnable);
                dicModel.Add("IsInitialPasswordChange", model.IsInitialPasswordChange);
                dicModel.Add("Language", model.Language);
                dicModel.Add("IsApprove", model.IsApprove);
                dicModel.Add("Photo", model.Photo);
                dicModel.Add("IsNewUser", model.IsNewUser);
                dicModel.Add("UserType", model.UserType);
                dicModel.Add("RoleList", model.RoleList);
                dicModel.Add("Position", model.Position);
                dicModel.Add("RateType", model.RateType);
                dicModel.Add("City", model.City);
                dicModel.Add("Province", model.Province);
                dicModel.Add("Address", model.Address);
                dicModel.Add("Zip", model.Zip);
                dicModel.Add("AssetClass", model.AssetClass);
                dicModel.Add("Password", model.Password);
                dicModel.Add("CreatedBy", model.CreatedBy);
                dicModel.Add("InsertedDate", model.InsertedDate);

                await _collection.InsertOneAsync(dicModel);

                return model.Did;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public async Task<bool> UpdateAsync(string collectionName, UserModel model)
        {
            try
            {
                UpdateResult data = null;
                var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<UserModel>(collectionName);
                var filter = Builders<UserModel>.Filter.Eq(s => s.Did, model.Did);
             
                if (!string.IsNullOrEmpty(model.Password))
                {
                    var update = Builders<UserModel>.Update
                   .Set(s => s.Email, model.Email)
                   .Set(s => s.IsVendor, model.IsVendor)
                   .Set("User Name", model.UserName)
                   .Set(s => s.Title, model.Title)
                   .Set("First Name", model.FirstName)
                   .Set("Last Name", model.LastName)
                   .Set(s => s.PropertyBuilding, model.PropertyBuilding)
                   .Set(s => s.PhoneWork, model.PhoneWork)
                   .Set(s => s.PhoneMobile, model.PhoneMobile)
                   .Set(s => s.EmployeeID, model.EmployeeID)
                   .Set(s => s.Rate, model.Rate)
                   .Set(s => s.IsCompanyOwner, model.IsCompanyOwner)
                   .Set(s => s.AutoLoginEnable, model.AutoLoginEnable)
                   .Set(s => s.IsInitialPasswordChange, model.IsInitialPasswordChange)
                   .Set(s => s.Language, model.Language)
                   .Set(s => s.IsApprove, model.IsApprove)
                   .Set(s => s.Photo, model.Photo)
                   .Set(s => s.IsNewUser, model.IsNewUser)
                   .Set(s => s.UserType, model.UserType)
                   .Set(s => s.RoleList, model.RoleList)
                   .Set(s => s.Position, model.Position)
                   .Set(s => s.RateType, model.RateType)
                   .Set(s => s.City, model.City)
                   .Set(s => s.Address, model.Address)
                   .Set(s => s.Province, model.Province)
                   .Set(s => s.Zip, model.Zip)
                   .Set(s => s.AssetClass, model.AssetClass)
                   .Set(s => s.UpdatedBy, model.UpdatedBy)
                   .Set(s => s.UpdatedDate, model.UpdatedDate)
                   .Set(s=>s.Password,model.Password);

                    data= await _collection.UpdateOneAsync(filter, update);
                }
                else
                {
                    var update = Builders<UserModel>.Update
                  .Set(s => s.Email, model.Email)
                  .Set(s => s.IsVendor, model.IsVendor)
                  .Set("User Name", model.UserName)
                  .Set(s => s.Title, model.Title)
                  .Set("First Name", model.FirstName)
                  .Set("Last Name", model.LastName)
                  .Set(s => s.PropertyBuilding, model.PropertyBuilding)
                  .Set(s => s.PhoneWork, model.PhoneWork)
                  .Set(s => s.PhoneMobile, model.PhoneMobile)
                  .Set(s => s.EmployeeID, model.EmployeeID)
                  .Set(s => s.Rate, model.Rate)
                  .Set(s => s.IsCompanyOwner, model.IsCompanyOwner)
                  .Set(s => s.AutoLoginEnable, model.AutoLoginEnable)
                  .Set(s => s.IsInitialPasswordChange, model.IsInitialPasswordChange)
                  .Set(s => s.Language, model.Language)
                  .Set(s => s.IsApprove, model.IsApprove)
                  .Set(s => s.Photo, model.Photo)
                  .Set(s => s.IsNewUser, model.IsNewUser)
                  .Set(s => s.UserType, model.UserType)
                  .Set(s => s.RoleList, model.RoleList)
                  .Set(s => s.Position, model.Position)
                  .Set(s => s.RateType, model.RateType)
                  .Set(s => s.City, model.City)
                  .Set(s => s.Address, model.Address)
                  .Set(s => s.Province, model.Province)
                  .Set(s => s.Zip, model.Zip)
                  .Set(s => s.AssetClass, model.AssetClass)
                  .Set(s => s.UpdatedBy, model.UpdatedBy)
                  .Set(s => s.UpdatedDate, model.UpdatedDate);

                  data=   await _collection.UpdateOneAsync(filter, update);
                }
              
                if (data.MatchedCount == 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Task<bool> DeleteAsync(string collectionName, string did)
        {
            throw new NotImplementedException();
        }


        public async Task<bool> IsExist(string collectionName, string userName, string did)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Dictionary<string, object>>(collectionName);
            bool isExist = false;

            var filterBuilder = Builders<Dictionary<string, object>>.Filter;
            var filter = filterBuilder.Eq("User Name", userName.Trim());
            if (!String.IsNullOrEmpty(did))
            {
                filter = filter & filterBuilder.Ne("Did", did);
            }
            var result = await _collection.Find(filter).AnyAsync();
            if (result)
            {
                isExist = true;
            }
            return isExist;
        }

        public async Task<IEnumerable<Dictionary<string, object>>> GetUserList(string collectionName, GetUserListByQuery query)
        {

            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Dictionary<string, object>>(collectionName);

            BsonDocument filter = new BsonDocument();
            if (!String.IsNullOrEmpty(query.QueryParam.Search))
            {

                filter = new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray().Add(
                        new BsonDocument("UserType", query.QueryParam.UserType.Trim())).Add(new BsonDocument("IsApprove", query.QueryParam.IsApprove.Trim()))
                    .Add(new BsonDocument("$or", new BsonArray{
                        new BsonDocument("First Name",new BsonDocument("$regex", query.QueryParam.Search).Add("$options", "i")),
                        new BsonDocument("Last Name",new BsonDocument("$regex",query.QueryParam.Search).Add("$options", "i")),
                        new BsonDocument("Email",new BsonDocument("$regex", query.QueryParam.Search).Add("$options", "i"))

                        }))) } };
            }
            else
            {
                filter = new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray()
                    .Add(new BsonDocument("UserType", query.QueryParam.UserType.Trim())).Add(new BsonDocument("IsApprove", query.QueryParam.IsApprove.Trim()))) } };
            }


            var pipeline = new BsonDocument[] {
                    filter,
                    new BsonDocument{ { "$project", new BsonDocument {
                       {"_id",0},{"Did",1},{"First Name",2},
                        {"Last Name",3},{"Email",4},{"InsertedDate",5}
                    }}},
                    new BsonDocument{ { "$skip", query.Paging.Skip}},
                    new BsonDocument{ { "$limit", query.Paging.Limit}}
                };


            var m = await _collection.AggregateAsync<Dictionary<string, object>>(pipeline);
            var result = await m.ToListAsync();


            return result;
        }

        public async Task<long> GetUserListTotalCount(string collectionName, GetUserListTotalByQuery query)
        {

            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Dictionary<string, object>>(collectionName);

            BsonDocument filter = new BsonDocument();
            if (!String.IsNullOrEmpty(query.QueryParam.Search))
            {

                filter = new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray().Add(
                        new BsonDocument("UserType", query.QueryParam.UserType.Trim())).Add(new BsonDocument("IsApprove", query.QueryParam.IsApprove.Trim()))
                    .Add(new BsonDocument("$or", new BsonArray{
                        new BsonDocument("First Name",new BsonDocument("$regex", query.QueryParam.Search).Add("$options", "i")),
                        new BsonDocument("Last Name",new BsonDocument("$regex",query.QueryParam.Search).Add("$options", "i")),
                        new BsonDocument("Email",new BsonDocument("$regex", query.QueryParam.Search).Add("$options", "i"))

                        }))) } };
            }
            else
            {
                filter = new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray()
                    .Add(new BsonDocument("UserType", query.QueryParam.UserType.Trim())).Add(new BsonDocument("IsApprove", query.QueryParam.IsApprove.Trim()))) } };
            }


            var pipeline = new BsonDocument[] {
                    filter
                };


            var m = await _collection.AggregateAsync<Dictionary<string, object>>(pipeline);
            var result = await m.ToListAsync();

            return result.Count();
        }

        public async Task<IEnumerable<Dictionary<string, object>>> GetResponsibilityReAssign(string collectionName, ResponsibilityReAssignQuery request)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Dictionary<string, object>>(collectionName);

            BsonDocument filter = new BsonDocument();
            if (!String.IsNullOrEmpty(request.Did))
            {

                filter = new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray().Add(
                        new BsonDocument("UserType", request.UserType.Trim())).Add(new BsonDocument("IsApprove", "1"))
                        .Add(new BsonDocument("Did",new BsonDocument("$ne",request.Did)))
                    .Add(new BsonDocument("$or", new BsonArray{
                        new BsonDocument("2",new BsonDocument("$regex", request.Search).Add("$options", "i"))
                        }))) } };
            }
            else
            {
                filter = new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray().Add(
                        new BsonDocument("UserType", request.UserType.Trim())).Add(new BsonDocument("IsApprove", "1"))
                    .Add(new BsonDocument("$or", new BsonArray{
                        new BsonDocument("2",new BsonDocument("$regex", request.Search).Add("$options", "i"))
                        }))) } };
            }

            var pipeline = new BsonDocument[] {
                    new BsonDocument{ { "$project", new BsonDocument {
                       {"_id",0},{"Did",1},
                        {"2",new BsonDocument("$concat",new BsonArray{"$First Name","$Last Name"})},
                        {"UserType","$UserType"},{"IsApprove","$IsApprove"}
                    }}},
                     new BsonDocument{ { "$sort", new BsonDocument("First Name",-1)} },
                     filter,
                    new BsonDocument{ { "$skip", request.Pagination.Skip}},
                    new BsonDocument{ { "$limit", request.Pagination.Limit}}
                };


            var m = await _collection.AggregateAsync<Dictionary<string, object>>(pipeline);
            var result = await m.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Dictionary<string, object>>> GetAPIIntegration(string collectionName)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Dictionary<string, object>>(collectionName);
            var filterBuilder = Builders<Dictionary<string, object>>.Filter;
            var filter = filterBuilder.Ne("Did", "");
            //var project = Builders<Dictionary<string, object>>.Projection.Exclude("_id").Exclude("ModId");
            var result = await _collection.Find(filter).ToListAsync();
            return result;
        }

        public async Task<Dictionary<string, object>> GetRolePermissionMapping(string collectionName, string roleDid)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Dictionary<string,object>>(collectionName);

            var pipeline = new BsonDocument[] {
                    new BsonDocument{ { "$match",  new BsonDocument("$and", new BsonArray().Add(new BsonDocument("Role", roleDid))) } },
                    new BsonDocument{ { "$project", new BsonDocument {
                        {"_id",0},{"UpdatedDate",0}
                    }}}
                };
            var m = await _collection.AggregateAsync<Dictionary<string, object>>(pipeline);

            return await m.FirstOrDefaultAsync();
        }

        public async Task<bool> SaveRolePermissionMapping(string collectionName, RolePermissionMappingModel model)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<RolePermissionMappingModel>(collectionName);
            await _collection.InsertOneAsync(model);

            return true;

        }

        public async Task<bool> UpdateRolePermissionMapping(string collectionName, RolePermissionMappingModel model)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<RolePermissionMappingModel>(collectionName);
            var filter = Builders<RolePermissionMappingModel>.Filter.Eq(s => s.Did, model.Did);

            var update = Builders<RolePermissionMappingModel>.Update
                .Set(s => s.Role, model.Role)
                .Set(s => s.UserPermission, model.UserPermission)
                .Set(s => s.UpdatedBy, model.UpdatedBy)
                .Set(s => s.UpdatedDate, model.UpdatedDate);

            await _collection.UpdateOneAsync(filter, update);

            return true;
        }

        public async Task<IEnumerable<Dictionary<string, object>>> GetRoleUserMapping(string collectionName, string[] roleDid)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Dictionary<string, object>>(collectionName);
            var filterBuilder = Builders<Dictionary<string, object>>.Filter;
            var filter = filterBuilder.In("RoleID", roleDid);
            return await _collection.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Dictionary<string, object>>> GetRolePermissionUserMapping(string collectionName, string[] roleDid)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Dictionary<string, object>>(collectionName);
            var filterBuilder = Builders<Dictionary<string, object>>.Filter;
            var filter = filterBuilder.In("Role", roleDid);
            var result = await _collection.FindAsync(filter);
            return result.ToList();
        }

        public async Task<IEnumerable<Dictionary<string, object>>> GetUserPermissionAsync(string collectionName, string[] permissionDid)
        {
            List<Dictionary<string, object>> listDic = new List<Dictionary<string, object>>();

            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Dictionary<string, object>>(collectionName);
            var match = new BsonDocument
            {
                 {"$match",new BsonDocument("Did",new BsonDocument("$in",new BsonArray(permissionDid))) },
            };

            var group = new BsonDocument
            {
                { "$group",new BsonDocument{
                         { "_id", new BsonDocument {{ "AppId", "$AppId" }, { "Did", "$Did" }, { "PermissionName", "$PermissionName" }, { "PermissionKey", "$PermissionKey" } } } } 
                } 
            };

            var project = new BsonDocument
            {
                { "$project", new BsonDocument {
                    {"Did","$Did"},{"AppId","$AppId" },{"PermissionName","$PermissionName" },{"PermissionKey","$PermissionKey"}
                 }}
            };
   
                
            var pipeline = new BsonDocument[] {
                match,group,project
            };

            var m = await _collection.AggregateAsync<Dictionary<string, object>>(pipeline);
            //var result = await m.ToListAsync();

            foreach (var item in await m.ToListAsync())
            {
                listDic.Add(item.Values.FirstOrDefault().ToBsonDocument().ToDictionary());
            }

            return listDic;
        }

        public async Task<IEnumerable<Dictionary<string, object>>> GetUserAsync(string collectionName, string[] userIds)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Dictionary<string, object>>(collectionName);
            var filterBuilder = Builders<Dictionary<string, object>>.Filter;
            var filter = filterBuilder.In("Did", userIds);
            var result = await _collection.FindAsync(filter);
            return result.ToList();
        }

        public async Task<bool> BulkInsertToUserPermission(string collectionName, List<Dictionary<string, object>> dataList)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Dictionary<string, object>>(collectionName);
            await _collection.InsertManyAsync(dataList);

            return true;
        }

        public async Task<IEnumerable<Dictionary<string, object>>> GetpremiseUserMapping(string collectionName, string[] usersDid)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Dictionary<string, object>>(collectionName);
            var filterBuilder = Builders<Dictionary<string, object>>.Filter;
            var filter = filterBuilder.In("UserID", usersDid);
            var result = await _collection.FindAsync(filter);
            return result.ToList();
        }

        public async Task<bool> DeleteUserPermission(string collectionName, string[] userDids, string[] appDids)
        {
            var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<UserPermissionCollectionModel>(collectionName);
            var filterBuilder = Builders<UserPermissionCollectionModel>.Filter;
            var query = filterBuilder.In(m => m.UserId, userDids);
            query = query & filterBuilder.In(m => m.AppId, appDids);
            await _collection.DeleteOneAsync(query);

            return true;
        }
    }
}
