using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ado.Net.EF.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace DBConnect
{

    public class JsonParse
    {
        public readonly IApplicationConfig _applicationConfig;

        public JsonParse(IApplicationConfig applicationConfig)
        {
            _applicationConfig = applicationConfig;
        }

        public async void SerializationFile()
        {
            var filePath = "..\\..\\..\\..\\categories.json";
            var categories = _applicationConfig.GetCategories();
            
            var categoriesResult = categories
                .Select(x => new
                {
                    x.CategoryID, x.CategoryName, x.Description, x.Picture
                })
                .ToList();
            
            string json = JsonConvert.SerializeObject(categoriesResult);
            File.WriteAllText(filePath, json);
        }

        public List<JObject> DeserializationFile()
        {
            var filePath = "..\\..\\..\\..\\categories.json";
            List<JObject> list = JsonConvert.DeserializeObject<List<JObject>>(File.ReadAllText(filePath));

            return list;
        }
        
        /*public  void DeserializationFile()
        {
            var model = new Model();
            var entityTypes = new List<Type>
            {
                typeof(Categories),
            };
            var dataNS = $"..\\..\\..\\..\\";
            foreach (var type in entityTypes)
            {
                var dataFile = $"categories.json";
                var json = GetResource(dataFile);
                var listType = typeof(IEnumerable<>).MakeGenericType(type);
                var entities = JsonConvert.DeserializeObject(json, listType) as IEnumerable;
                var dbset = model.Set(type);
                /*dbset.RemoveRange(dbset);
                dbset.AddRange(entities as IEnumerable);
                try
                {
                    model.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }#1#
            }
        }*/

        /*private string GetResource(string resourceName)
        {
            var result = string.Empty;
            var assembly = typeof(Configuration).Assembly;
            using (FileStream fs = new FileStream(resourceName, FileMode.OpenOrCreate))
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream, Encoding.GetEncoding("iso-8859-1")))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }*/
    }
}