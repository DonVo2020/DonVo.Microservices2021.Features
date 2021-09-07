﻿namespace DonVo.EventSourcing.MongoDB.ProductsAPI.Settings.ProductDatabase
{
    public class ProductDatabaseSettings : IProductDatabaseSettings, ISettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
