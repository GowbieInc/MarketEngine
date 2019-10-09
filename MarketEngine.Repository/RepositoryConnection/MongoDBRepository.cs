using MarketEngine.Model.Models.Configuration;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace MarketEngine.Repository.RepositoryConnection
{
    public class MongoDBRepository
    {
        protected IMongoDatabase Database;

        public MongoDBRepository()
        {
            var mongoSettings = DependencyInjection.Configuration.ConfigurationProvider.GetOptions<MongoSettings>(); 
            GetDatabase(mongoSettings);
            RegisterConventions();
        }

        private void GetDatabase(MongoSettings options)
        {
            var mongoClient = new MongoClient(options.ConnectionString);
            Database = mongoClient.GetDatabase(options.Database);
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
