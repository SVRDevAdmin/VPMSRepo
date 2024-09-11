using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json;

namespace VPMSWeb.Lib.Settings
{
    public class ResponseCodeRepo
    {
        public static Dictionary<String, String> GetResponseCodeDictionary()
        {
            String sJson = File.ReadAllText("Lib/ResponseCode/ErrorCode.json");
            Dictionary<String, String> RespCodeDictionary = JsonConvert.DeserializeObject<Dictionary<String, String>>(sJson);

            return RespCodeDictionary;
        }
    }
}
