using AutoMapper;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Mappers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, UserResponse>();
            CreateMap<Employee, EmployeeDetailResponse>();
            CreateMap<Employee, EmployeeListResponse>();
            CreateMap<Supplier, SupplierDetailResponse>();
            CreateMap<Department, DepartmentResponse>();
            CreateMap<Department, DepartmentDetailResponse>();
            CreateMap<InventoryItem, InventoryItemDetailResponse>();
            CreateMap<InventoryItem, InventoryItemListResponse>();
            CreateMap<Message, MessageResponse>();
            CreateMap<CollectionPoint, CollectionPointListResponse>();
        }
    }
}
