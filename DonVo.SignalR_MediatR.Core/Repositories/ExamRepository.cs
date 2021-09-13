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
    public class ExamRepository : IExamRepository
    {
        private readonly DataContext _context;

        public ExamRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Exam>> GetExamsAsync(string userId, int? subjectId, CancellationToken cancellationToken = default)
        {
            var query = _context.Exams.Where(x => x.UserId == userId).AsNoTracking().AsQueryable();
            if (subjectId.HasValue)
            {
                query = query.Where(x => x.SubjectId == subjectId);
            }

            return await query.OrderByDescending(x => x.DueDate).ToListAsync(cancellationToken);
        }

        public async Task<Exam> GetExamAsync(int examId, string userId, CancellationToken cancellationToken = default)
        {
            return await _context.Exams.FirstOrDefaultAsync(x => x.Id == examId && x.UserId == userId, cancellationToken);
        }

        public void RemoveExam(Exam exam)
        {
            _context.Exams.Remove(exam);
        }

        public async Task AddExamAsync(Exam exam, CancellationToken cancellationToken)
        {
            await _context.AddAsync(exam, cancellationToken);
        }
    }
}
