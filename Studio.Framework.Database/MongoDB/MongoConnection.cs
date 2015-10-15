using System.Configuration;

namespace Studio.Framework.Database.MongoDB
{

    public class MongoConnection
    {
        /// <summary>
        /// MongoDB 커넥션 스트링 정보를 가져올 수 있습니다.
        /// </summary>
        /// <param name="connectionName">커넥션명</param>
        /// <returns></returns>
        public static MongoConnectionElement MongoDB(string connectionName)
        {
            var config = (MongoConfiguration)ConfigurationManager.GetSection("MongoDB");
            return config.Get[connectionName];
        }
    }

    public class MongoConnectionElement : ConfigurationElement
    {
        private static readonly ConfigurationProperty _connectionName = new ConfigurationProperty("ConnectionName", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);
        private static readonly ConfigurationProperty _database = new ConfigurationProperty("Database", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);
        private static readonly ConfigurationProperty _address = new ConfigurationProperty("Address", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);
        private static readonly ConfigurationProperty _port = new ConfigurationProperty("Port", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);
        private static readonly ConfigurationProperty _userName = new ConfigurationProperty("UserName", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);
        private static readonly ConfigurationProperty _password = new ConfigurationProperty("Password", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);
        private static readonly ConfigurationProperty _option = new ConfigurationProperty("Option", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);

        /// <summary>
        /// MongoDB ConnectionString 생성자
        /// </summary>
        public MongoConnectionElement()
        {
            base.Properties.Add(_connectionName);
            base.Properties.Add(_database);
            base.Properties.Add(_address);
            base.Properties.Add(_port);
            base.Properties.Add(_userName);
            base.Properties.Add(_password);
            base.Properties.Add(_option);
        }

        /// <summary>
        /// 커넥션명을 가져올 수 있는 속성입니다.
        /// </summary>
        [ConfigurationProperty("ConnectionName", IsRequired = true)]
        public string ConnectionName
        {
            get { return (string)this[_connectionName]; }
        }

        /// <summary>
        /// 커넥션명을 가져올 수 있는 속성입니다.
        /// </summary>
        [ConfigurationProperty("Database", IsRequired = true)]
        public string Database
        {
            get { return (string)this[_database]; }
        }

        /// <summary>
        /// DB Address를 가져올 수 있습니다.
        /// </summary>
        [ConfigurationProperty("Address", IsRequired = true)]
        public string Address
        {
            get { return (string)this[_address]; }
        }

        /// <summary>
        /// Port를 가져올 수 있는 속성입니다.
        /// </summary>
        [ConfigurationProperty("Port", IsRequired = true)]
        public string Port
        {
            get { return (string)this[_port]; }
        }

        /// <summary>
        /// 계정(아이디)을 가져올 수 있는 속성입니다.
        /// </summary>
        [ConfigurationProperty("UserName", IsRequired = true)]
        public string UserName
        {
            get { return (string)this[_userName]; }
        }

        /// <summary>
        /// Name
        /// </summary>
        [ConfigurationProperty("Password", IsRequired = true)]
        public string Password
        {
            get { return (string)this[_password]; }
        }

        /// <summary>
        /// Name
        /// </summary>
        [ConfigurationProperty("Option", IsRequired = true)]
        public string Option
        {
            get { return (string)this[_option]; }
        }
    }

    /// <summary>
    /// 컬렉션 클래스입니다.
    /// </summary>
    [ConfigurationCollection(typeof(MongoConnectionElement), AddItemName = "MongoDBConnectionString", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class MongoDBCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// 엘리먼트 객체를 생성합니다.
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new MongoConnectionElement();
        }

        /// <summary>
        /// 엘리먼트 키를 가져올 수 있습니다.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MongoConnectionElement)element).ConnectionName;
        }

        /// <summary>
        /// ConnectionName에 해당하는 엘리먼트를 생성합니다.
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        new public MongoConnectionElement this[string connectionName]
        {
            get { return (MongoConnectionElement)BaseGet(connectionName); }
        }
    }

    /// <summary>
    /// Configutration Section 클래스입니다.
    /// </summary>
    public class MongoConfiguration : ConfigurationSection
    {

        private static readonly ConfigurationProperty _mongoConnectionElement = new ConfigurationProperty("MongoDBConnectionStrings", typeof(MongoDBCollection), null, ConfigurationPropertyOptions.IsRequired);

        /// <summary>
        /// Initialize
        /// </summary>
        public MongoConfiguration()
        {

            base.Properties.Add(_mongoConnectionElement);
        }

        /// <summary>
        /// Messages Collection
        /// </summary>
        [ConfigurationProperty("MongoDBConnectionStrings", IsRequired = true)]
        public MongoDBCollection Get
        {
            get { return (MongoDBCollection)this[_mongoConnectionElement]; }
        }
    }
    
}
