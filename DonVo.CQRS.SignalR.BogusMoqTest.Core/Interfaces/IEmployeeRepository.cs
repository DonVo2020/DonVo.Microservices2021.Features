using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces
{
    public interface IEmployeeRepository
    {
        public void RemoveEmployee(Employee employee);
        public Task<Employee> GetEmployeeAsync(int employeeId, string departmentId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Employee>> GetEmployeesAsync(int? departmentId, CancellationToken cancellationToken = default);
        Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken);
    }
}
