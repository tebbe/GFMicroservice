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
    public class PropertiesDal
    {
        private readonly IDBContext _dbContext;

        public PropertiesDal(IDBContext dbContext)
        {
            _dbContext = dbContext;
            if (!BsonClassMap.IsClassMapRegistered(typeof(Inventory_Property)))
            {
                BsonClassMap.RegisterClassMap<Inventory_Property>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }

        public async Task<IEnumerable<Dictionary<string, object>>> GetPropertyWithBuilding(string buildingCollectionName, string collectionName, string name)
        {
            try
            {
                var _inventorypropertyCollection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<Inventory_Property>(collectionName);

                BsonDocument filter = new BsonDocument{ { "$match",
                    new BsonDocument("$or", new BsonArray().Add(new BsonDocument("PropertyName", new BsonDocument("$regex", name ).Add("$options", "i")))
                    .Add(new BsonDocument("BuildingName", new BsonDocument("$regex",  name ).Add("$options", "i")))) } };

                var pipeline = new BsonDocument[] {
                    new BsonDocument("$match",
                    new BsonDocument("IsActive", "1")),
                    new BsonDocument("$lookup",
                    new BsonDocument
                        {
                            { "from", buildingCollectionName.ToString() },
                            { "localField", "Did" },
                            { "foreignField", "Property" },
                            { "as", "results" }
                        }),

                        new BsonDocument("$project",
                        new BsonDocument
                            {
                                { "_id", 0 },
                                { "Property", "$Did" },
                                { "PropertyName", "$Name" },
                                { "results",
                        new BsonDocument("$filter",
                        new BsonDocument
                                    {
                                        { "input", "$results" },
                                        { "as", "item" },
                                        { "cond",
                                        new BsonDocument("$gt",new BsonArray
                                        {
                                            new BsonDocument("$indexOfCP",
                                            new BsonArray
                                                {
                                                    new BsonDocument("$toLower", "$$item.Active"),"1"
                                                }),
                                            -1
                                        }) }
                                }) }
                        }),

                    new BsonDocument("$project",
                    new BsonDocument
                        {
                            { "Property", 1 },
                            { "PropertyName", 1 },
                            { "BuildingIDs", "$results.Did" },
                            { "BuildingName", "$results.BuildingName" }
                        })
                    ,filter

                };


                var m = await _inventorypropertyCollection.AggregateAsync<Dictionary<string, object>>(pipeline);
                var result = await m.ToListAsync();
                return result;
            }
            catch
            {
                throw;
            }
        }

    }
}
