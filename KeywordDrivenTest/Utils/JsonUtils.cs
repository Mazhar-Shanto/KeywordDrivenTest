using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace KeywordDrivenTest.Utils
{
    public class JsonUtils
    {
        public static Dictionary<string, string> ReadLocators(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }
    }
}
