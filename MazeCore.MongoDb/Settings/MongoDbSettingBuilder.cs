namespace MazeCore.MongoDb.Settings
{
    public class MongoDbSettingBuilder
    {
        private readonly IMongoDbSetting _setting;
        internal MongoDbSettingBuilder()
        {
            _setting = new MongoDbSetting();
        }

        public MongoDbSettingBuilder AddConnectionString(string connectionString)
        {
            _setting.ConnectionString = connectionString;
            return this;
        }

        public MongoDbSettingBuilder AddDatabaseName(string databaseName)
        {
            _setting.DatabaseName = databaseName;
            return this;
        }

        public IMongoDbSetting Build()
        {
            return _setting;
        }
    }
}
