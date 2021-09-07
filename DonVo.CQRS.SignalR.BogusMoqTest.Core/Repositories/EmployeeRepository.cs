using DonVo.CQRS.SignalR.BogusMoqTest.Core.Data;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;

        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }

        public void RemoveEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
        }

        public async Task<Employee> GetEmployeeAsync(int employeeId, string departmentId, CancellationToken cancellationToken = default)
        {
            if (!string.IsNullOrWhiteSpace(departmentId))
                return await _context.Employees.FirstOrDefaultAsync(x => x.Id == employeeId && x.DepartmentId == departmentId, cancellationToken);

            return await _context.Employees.FirstOrDefaultAsync(x => x.Id == employeeId, cancellationToken);
        }

        public async Task AddEmployeeAsync(Employee employee, CancellationToken cancellationToken)
        {
            await _context.AddAsync(employee, cancellationToken);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(int? departmentId, CancellationToken cancellationToken = default)
        {
            var query = _context.Employees.AsNoTracking().AsQueryable();
            if (departmentId.HasValue)
            {
                query = query.Where(x => x.DepartmentId == departmentId.Value.ToString());
            }

            return await query.OrderByDescending(x => x.Name).ToListAsync(cancellationToken);
        }
    }
}
