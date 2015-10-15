using System.Configuration;

namespace Studio.Framework.Database.MongoDB
{
    public class MongoCollection
    {
        /// <summary>
        /// CollectionElement 형식의 이름을 가져올 수 있습니다.
        /// </summary>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        public static MongoCollectionElement Name(string collectionName)
        {
            var config = (MongoCollectionConfiguration)ConfigurationManager.GetSection("MongoDBCollection");
            return config.Get[collectionName];
        }

        /// <summary>
        /// MongoDBCollectionElement 클래스
        /// </summary>
        public class MongoCollectionElement : ConfigurationElement
        {
            private static readonly ConfigurationProperty _collectionName = new ConfigurationProperty("CollectionName", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);
            /// <summary>
            /// MongoDB ConnectionString 생성자
            /// </summary>
            public MongoCollectionElement()
            {
                base.Properties.Add(_collectionName);
            }

            /// <summary>
            /// 커넥션명을 가져올 수 있는 속성입니다.
            /// </summary>
            [ConfigurationProperty("CollectionName", IsRequired = true)]
            public string CollectionName
            {
                get { return (string)this[_collectionName]; }
            }

        }

        /// <summary>
        /// MongoDBCollectionOfCollection 클래스
        /// </summary>
        [ConfigurationCollection(typeof(MongoCollectionElement), AddItemName = "Collection", CollectionType = ConfigurationElementCollectionType.BasicMap)]
        public class MongoDBCollectionOfCollection : ConfigurationElementCollection
        {
            /// <summary>
            /// 새로운 엘리먼트를 생성합니다.
            /// </summary>
            /// <returns></returns>
            protected override ConfigurationElement CreateNewElement()
            {
                return new MongoCollectionElement();
            }

            /// <summary>
            /// 엘리멘트 키를 기반으로 컬렉션명을 가져옵니다.
            /// </summary>
            /// <param name="element">엘리먼트 이름</param>
            /// <returns></returns>
            protected override object GetElementKey(ConfigurationElement element)
            {
                return ((MongoCollectionElement)element).CollectionName;
            }

            /// <summary>
            /// 본인의 엘리먼트 이름으로 새로운 컬렉션명을 생성합니다.
            /// </summary>
            /// <param name="collectionName">켈렉션 이름</param>
            /// <returns></returns>
            new public MongoCollectionElement this[string collectionName]
            {
                get { return (MongoCollectionElement)BaseGet(collectionName); }
            }
        }

        /// <summary>
        /// MongoDBCollectionConfiguration
        /// </summary>
        public class MongoCollectionConfiguration : ConfigurationSection
        {
            /// <summary>
            /// MongoDB 엘리먼트 속성
            /// </summary>
            private static readonly ConfigurationProperty _mongoCollectionElement = new ConfigurationProperty("MongoDBCollections", typeof(MongoDBCollectionOfCollection), null, ConfigurationPropertyOptions.IsRequired);

            /// <summary>
            /// 생성자
            /// </summary>
            public MongoCollectionConfiguration()
            {

                base.Properties.Add(_mongoCollectionElement);
            }

            /// <summary>
            /// Messages Collection
            /// </summary>
            [ConfigurationProperty("MongoDBCollections", IsRequired = true)]
            public MongoDBCollectionOfCollection Get
            {
                get { return (MongoDBCollectionOfCollection)this[_mongoCollectionElement]; }
            }
        }
    }
}
