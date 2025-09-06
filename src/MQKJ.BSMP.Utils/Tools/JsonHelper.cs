using Newtonsoft.Json;
namespace MQKJ.BSMP.Utils.Tools
{
    public static class JsonHelper
    {
        public static string GetJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static object GetObject(string obj)
        {
            return JsonConvert.DeserializeObject(obj);
        }
    }
}
