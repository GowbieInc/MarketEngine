using MarketEngine.Model.Models.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketEngine.Repository
{
    public class MongoDBStartup
    {
        protected MongoDatabase MongoDatabase;
        public MongoDBStartup(IOptions<MongoSettings> options)
        {
            var connectionString = options.Value.ConnectionString;
            var database = options.Value.Database;
            var connection = string.Format(connectionString, options.Value.User, options.Value.Password);

            var mongoClient = new MongoClient(connection);
            var mongoDBSettings = new MongoDatabaseSettings(); 

        }
    }
}
