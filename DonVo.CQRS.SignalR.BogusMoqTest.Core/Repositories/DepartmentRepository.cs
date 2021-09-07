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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DataContext _context;

        public DepartmentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddDepartmentAsync(Department department, CancellationToken cancellationToken)
        {
            await _context.Departments.AddAsync(department, cancellationToken);
        }

        public async Task<Department> GetDepartmentAsync(int departmentId, CancellationToken cancellationToken = default)
        {
            return await _context.Departments.FirstOrDefaultAsync(x => x.Id == departmentId.ToString(), cancellationToken);
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Departments.OrderBy(x => x.Name).ToListAsync(cancellationToken);
        }

        public void RemoveDepartment(Department department)
        {
            _context.Departments.Remove(department);
        }
    }
}
