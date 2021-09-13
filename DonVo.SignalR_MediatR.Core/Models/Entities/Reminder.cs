using System;
using DonVo.SignalR_MediatR.Core.Helpers;

namespace DonVo.SignalR_MediatR.Core.Models.Entities
{
    public class Reminder : BaseOwnedEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Priority Priority { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
