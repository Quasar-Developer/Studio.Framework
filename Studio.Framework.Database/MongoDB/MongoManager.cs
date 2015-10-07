using MongoDB.Driver;

namespace Studio.Framework.Database
{
    public class MongoManager<T>
    {
        protected IMongoClient _client;
        protected IMongoDatabase _dataBase;

        public MongoDBManager()
        {
            MongoCredential credential = MongoCredential.CreateMongoCRCredential(databaseName, username, )
            MongoClientSettings settings = new MongoClientSettings
            {
                Credentials = new[] {  },
            };
            
            _client = new MongoClient(settings);
        }
    }
}
