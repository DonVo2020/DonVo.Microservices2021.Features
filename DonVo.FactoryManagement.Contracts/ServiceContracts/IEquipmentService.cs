using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Equipment;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface IEquipmentService
    {
        Task<WrapperEquipmentListVM> GetListPaged(GetDataListVM dataListVM);
        Task<WrapperEquipmentListVM> Add(EquipmentVM vm);
        Task<WrapperEquipmentListVM> Update(string id, EquipmentVM vm);
        Task<WrapperEquipmentListVM> Delete(EquipmentVM itemTemp);
    }
}
