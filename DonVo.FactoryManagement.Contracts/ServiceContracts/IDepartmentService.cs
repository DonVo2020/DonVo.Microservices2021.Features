using DonVo.FactoryManagement.Models.ViewModels;
using DonVo.FactoryManagement.Models.ViewModels.Department;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts.ServiceContracts
{
    public interface IDepartmentService
    {
        Task<WrapperDepartmentListVM> GetListPaged(GetDataListVM dataListVM);
        Task<WrapperDepartmentListVM> Add(DepartmentVM vm);
        Task<WrapperDepartmentListVM> Update(string id, DepartmentVM vm);
        Task<WrapperDepartmentListVM> Delete(DepartmentVM itemTemp);
    }
}
