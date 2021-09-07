namespace DonVo.EventSourcing.MongoDB.SourcingAPI.Settings.SourcingDatabase
{
    public interface ISourcingDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
