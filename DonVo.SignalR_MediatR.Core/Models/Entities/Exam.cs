using System;

namespace DonVo.SignalR_MediatR.Core.Models.Entities
{
    public class Exam : BaseOwnedEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public int SubjectId { get; set; }
    }
}
