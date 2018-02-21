using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileImporter
{
    public class ConfigurationManager
    {
        private const string FILE_NAME = ".\\FileImporterConfig.json";

        public Configuration OpenConfiguration()
        {
            if (!File.Exists(FILE_NAME))
                return new Configuration();

            var str = File.ReadAllText(FILE_NAME);

            if (string.IsNullOrWhiteSpace(str))
                return new Configuration();

            JObject jObject = JObject.Parse(str);
            var jsonSerializer = new JsonSerializer();
            var configObject = (Configuration)jsonSerializer.Deserialize(new JTokenReader(jObject), typeof(Configuration));
            return configObject;
        }

        public void SaveConfiguration(Configuration config)
        {
            var jObject = JObject.FromObject(config);
            File.WriteAllText(FILE_NAME, jObject.ToString());
        }
    }
}
