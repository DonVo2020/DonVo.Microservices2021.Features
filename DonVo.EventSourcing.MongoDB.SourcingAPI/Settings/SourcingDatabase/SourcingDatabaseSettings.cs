namespace DonVo.EventSourcing.MongoDB.SourcingAPI.Settings.SourcingDatabase
{
    public class SourcingDatabaseSettings : ISourcingDatabaseSettings, ISettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
