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
    public class EquipmentsDal
    {
        private readonly IDBContext _dbContext;

        public EquipmentsDal(IDBContext dbContext)
        {
            _dbContext = dbContext;
            if (!BsonClassMap.IsClassMapRegistered(typeof(PMDemoScheduleModel)))
            {
                BsonClassMap.RegisterClassMap<PMDemoScheduleModel>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }

        public async Task<IEnumerable<Dictionary<string, object>>> GetPmDemoScheduleWithScheduleInstant(string collectionName, string scheduleInstantCollectionName)
        {
            try
            {
                var _collection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<PMDemoScheduleModel>(collectionName);

                var pipeline = new BsonDocument[] {
                    new BsonDocument{{ "$lookup", new BsonDocument("from",scheduleInstantCollectionName)
                                   .Add("localField","ScheduleID").Add("foreignField","ScheduleID").Add("as","PMSInstant") }},
                    new BsonDocument{ { "$match", new BsonDocument("$and", new BsonArray().Add(new BsonDocument("Status", new BsonDocument("$eq", "")))) } },
                    new BsonDocument{{"$unwind", new BsonDocument("path", "$PMSInstant").Add("preserveNullAndEmptyArrays", true) }},
                    new BsonDocument{ { "$project", new BsonDocument {
                        {"Did","$Did"},
                        {"Status","$Status" },
                        {"ScheduleID","$ScheduleID" },
                        {"PMSInstantDid","$PMSInstant.Did" },
                        {"PMSInsTantScheduleID","$PMSInstant.ScheduleID" },
                        {"PMSInstEquipmentID","$PMSInstant.EquipmentID" },
                        {"PMSIntSerial","$PMSInstant.Serial" },
                        {"BuildingID","$PMSInstant.BuildingID" },
                        {"FloorID","$PMSInstant.FloorID" },
                        {"FloorNo","$PMSInstant.FloorNo" },
                        {"SuiteID","$PMSInstant.SuiteID" },
                        {"SuiteNo","$PMSInstant.SuiteNo" },
                        {"Serial","Serial" }
                    }}}
                };
                var m = await _collection.AggregateAsync<Dictionary<string, object>>(pipeline);
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
