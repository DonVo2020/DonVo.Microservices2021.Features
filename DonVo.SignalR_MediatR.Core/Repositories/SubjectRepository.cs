using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DonVo.SignalR_MediatR.Core.Data;
using DonVo.SignalR_MediatR.Core.Interfaces;
using DonVo.SignalR_MediatR.Core.Models.Entities;

namespace DonVo.SignalR_MediatR.Core.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DataContext _context;

        public SubjectRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddSubjectAsync(Subject subject, CancellationToken cancellationToken)
        {
            await _context.AddAsync(subject, cancellationToken);
        }

        public async Task<IEnumerable<Subject>> GetSubjectsAsync(string userId, CancellationToken cancellationToken)
        {
            return await _context.Subjects.Include(x => x.Grade)
                .Where(x => x.UserId == userId).AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Subject> GetSubjectByIdAsync(int id, string userId, CancellationToken cancellationToken)
        {
            return await _context.Subjects.Include(x => x.Grade)
                .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId, cancellationToken);
        }

        public async Task<bool> SubjectExistsAsync(int subjectId, string userId, CancellationToken cancellationToken = default)
        {
            return await _context.Subjects.AnyAsync(x => x.Id == subjectId && x.UserId == userId, cancellationToken);
        }
    }
}
