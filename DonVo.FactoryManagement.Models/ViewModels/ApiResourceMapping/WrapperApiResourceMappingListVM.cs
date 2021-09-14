using System.Collections.Generic;

namespace DonVo.FactoryManagement.Models.ViewModels.ApiResourceMapping
{
    public class WrapperApiResourceMappingListVM
    {
        public WrapperApiResourceMappingListVM()
        {
            ListOfData = new List<ApiResourceMappingVM>();
        }

        public long TotalRecords { get; set; }

        public List<ApiResourceMappingVM> ListOfData { get; set; }
    }
}
