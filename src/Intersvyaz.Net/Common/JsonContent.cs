using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;

namespace Intersvyaz.Net.Common
{
    public class JsonContent : StringContent
    {
        public JsonContent(object value) : base(Serialize(value), Encoding.UTF8, "application/json") { }

        private static string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            });
        }
    }
}
