
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace EFCore.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EFCore.Model.NorthwindEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}