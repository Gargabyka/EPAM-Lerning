using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using Ado.Net.EF.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ado.Net.EF.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Model>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Model modelBuilder)
        {
            var territory = modelBuilder.Territories.ToList();
            
            modelBuilder.Categories.AddOrUpdate(
                h =>h.CategoryID,
                new Categories
                {
                    CategoryID = 1,
                    CategoryName = "Beverages",
                    Description = "Soft drinks, coffees, teas, beers, and ales"
                },
                new Categories
                {
                    CategoryID = 2,
                    CategoryName = "Condiments",
                    Description = "Sweet and savory sauces, relishes, spreads, and seasonings"
                },
                new Categories
                {
                    CategoryID = 3,
                    CategoryName = "Confections",
                    Description = "Desserts, candies, and sweet breads"
                },
                new Categories
                {
                    CategoryID = 4,
                    CategoryName = "Dairy Products",
                    Description = "Cheeses"
                },
                new Categories
                {
                    CategoryID = 5,
                    CategoryName = "Grains/Cereals",
                    Description = "Breads, crackers, pasta, and cereal"
                },
                new Categories
                {
                    CategoryID = 6,
                    CategoryName = "Meat/Poultry",
                    Description = "Prepared meats"
                },
                new Categories
                {
                    CategoryID = 7,
                    CategoryName = "Produce",
                    Description = "Dried fruit and bean curd"
                },
                new Categories
                {
                    CategoryID = 8,
                    CategoryName = "Seafood",
                    Description = "Seaweed and fish"
                });
            
            modelBuilder.Region.AddOrUpdate(
                h=>h.RegionID,
                new Region
                {
                    RegionID = 1,
                    RegionDescription = "Eastern"                                        
                },
                new Region
                {
                    RegionID = 2,
                    RegionDescription = "Western"   
                },
                new Region
                {
                    RegionID = 3,
                    RegionDescription = "Northern"   
                },
                new Region
                {
                    RegionID = 4,
                    RegionDescription = "Southern"   
                });

            var filePath = "territories.json";
            List<Territories> territories = JsonConvert.DeserializeObject<List<Territories>>(File.ReadAllText(filePath));
            
            foreach (var ter in territories)
            {
                modelBuilder.Territories.AddOrUpdate(
                    h=>h.TerritoryID,
                    new Territories
                    {
                        TerritoryID = ter.TerritoryID,
                        TerritoryDescription = ter.TerritoryDescription,
                        RegionID = ter.RegionID
                    });
            }
        }
        
        /*protected override void Seed(Model context)
        {
            var model = new Model();
            var entityTypes = new List<Type>
            {
                typeof(Categories),
                //typeof(Region),
                //typeof(Territories)
            };
            var dataNS = $"EntityFramework.Data";
            foreach (var type in entityTypes)
            {
                var dataFile = $"{type.Name.ToLower()}.json";
                var json = GetResource(dataFile);
                var listType = typeof(IEnumerable<>).MakeGenericType(type);
                var entities = JsonConvert.DeserializeObject(json, listType) as IEnumerable;
                var dbset = model.Set(type);
                dbset.RemoveRange(dbset);
                dbset.AddRange(entities as IEnumerable);
                try
                {
                    model.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private string GetResource(string resourceName)
        {
            var result = string.Empty;
            var assembly = typeof(Configuration).Assembly;
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream, Encoding.GetEncoding("iso-8859-1")))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }*/
    }
}
