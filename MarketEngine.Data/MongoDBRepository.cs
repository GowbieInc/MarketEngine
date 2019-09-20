using MarketEngine.Model.Models.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace MarketEngine.Repository
{
    public class MongoDBRepository
    {
        protected IMongoDatabase Database;
        public MongoDBRepository(IOptions<MongoSettings> options)
        {
            GetDatabase(options);
            RegisterConventions();
        }

        private void GetDatabase(IOptions<MongoSettings> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);
            Database = mongoClient.GetDatabase(options.Value.Database);
        }

        public static void RegisterConventions()
        {
            var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
            var ignoreExtraElementsConvention = new ConventionPack { new IgnoreExtraElementsConvention(true) };

            ConventionRegistry.Register("camel case", camelCaseConvention, t => true);
            ConventionRegistry.Register("IgnoreExtraElements", ignoreExtraElementsConvention, type => true);
        }
    }
}
