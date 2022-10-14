using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Utility
{
    public class Helper
    {
        public static void QueryGenerate<T1, T2>(List<FilterDefinition<T1>> list, T2 obj, Dictionary<string, string> type)
        {
            var builder = Builders<T1>.Filter;

            Func<string, string, object, bool> process = (name, type, value) =>
            {
                if (type == "in")
                {
                    var tempData = value.ToString();
                    if (tempData.Count() != 0)
                        list.Add(builder.In(name, tempData.Split(',')));
                }

                else if (type == "eq")
                {
                    if (!string.IsNullOrEmpty(value.ToString()))
                        list.Add(builder.Eq(name, value.ToString()));
                }
                else if (type == "ne")
                {
                    if (!string.IsNullOrEmpty(value.ToString()))
                        list.Add(builder.Ne(name, value.ToString()));
                }

                return true;
            };

            foreach (var props in obj.GetType().GetProperties())
            {

                var value = props.GetValue(obj, null);
                if (value != null)
                    process(props.Name.ToString(), type[props.Name.ToString()], value);
            };
        }

    }
}
