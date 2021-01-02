using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using API.Data.Collections;


namespace API.Data
{
    public class MongoDB
    {

        public IMongoDatabase DB { get; }

        public MongoDB(IConfiguration config)
        {
            try
            {
                var settings = MongoClientSettings.FromUrl(new MongoUrl(config["ConnectionString"]));
                var client = new MongoClient(settings);
                DB = client.GetDatabase(config["NomeBanco"]);
                MapClasses();
            }
            catch (Exception ex)
            {
                throw new MongoException("It was not possible to connect to MongoDB", ex);
            }
        }

        private void MapClasses()
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            if (!BsonClassMap.IsClassMapRegistered(typeof(Infectado)))
            {
                BsonClassMap.RegisterClassMap<Infectado>(i => {
                    i.AutoMap();
                    i.SetIgnoreExtraElements(true);
                });
            }
        }

    }
}