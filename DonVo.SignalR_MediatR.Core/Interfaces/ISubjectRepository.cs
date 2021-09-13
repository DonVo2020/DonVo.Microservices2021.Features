using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DonVo.SignalR_MediatR.Core.Models.Entities;
using DonVo.SignalR_MediatR.Core.Models.Responses;

namespace DonVo.SignalR_MediatR.Core.Interfaces
{
    public interface ISubjectRepository
    {
        /// <summary>
        /// Get subjects without tracking
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Subjects including related entities without tracking</returns>
        Task<IEnumerable<Subject>> GetSubjectsAsync(string userId, CancellationToken cancellationToken);
        Task AddSubjectAsync(Subject subject, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a subject by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns>A subject</returns>
        Task<Subject> GetSubjectByIdAsync(int id, string userId, CancellationToken cancellationToken);

        /// <summary>
        /// Checks whether a subject with the specified subject id and user id exists
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="userId"></param>
        /// <returns>True or false whether the subject exists or not</returns>
        Task<bool> SubjectExistsAsync(int subjectId, string userId, CancellationToken cancellationToken = default);
    }
}
