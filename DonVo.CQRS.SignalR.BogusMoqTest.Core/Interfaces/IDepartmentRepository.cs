using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces
{
    public interface IDepartmentRepository
    {
        public void RemoveDepartment(Department department);
        public Task<Department> GetDepartmentAsync(int departmentId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Department>> GetDepartmentsAsync(CancellationToken cancellationToken = default);
        Task AddDepartmentAsync(Department department, CancellationToken cancellationToken);
    }
}
