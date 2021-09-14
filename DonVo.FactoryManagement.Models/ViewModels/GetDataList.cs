namespace DonVo.FactoryManagement.Models.ViewModels
{
    public class GetDataListVM
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalRows { get; set; }
        public string FactoryId { get; set; }
        public string GlobalFilter { get; set; }
    }
}
