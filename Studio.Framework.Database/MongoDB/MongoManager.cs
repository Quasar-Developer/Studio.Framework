using MongoDB.Driver;
using Studio.Framework.Database.MongoDB;

namespace Studio.Framework.Database
{
    public class MongoManager<T>
    {
        protected IMongoClient _client;
        protected IMongoDatabase _dataBase;

        public MongoDBManager(string connectionName)
        {
            string dataBase = MongoConnection.MongoDB(connectionName).Database;
            string userName = MongoConnection.MongoDB(connectionName).UserName;
            string password = MongoConnection.MongoDB(connectionName).Password;
            string host = MongoConnection.MongoDB(connectionName).Address;
            

            int port = int.TryParse(MongoConnection.MongoDB(connectionName).Port, out port);

            //SecureString
            MongoCredential credential = MongoCredential.CreateMongoCRCredential(dataBase, userName, password)
            MongoClientSettings settings = new MongoClientSettings
            {
                Credentials = new[] { credential },
                Server = new MongoServerAddress(host, port)
            };
            
            _client = new MongoClient(settings);


        }
    }
}
