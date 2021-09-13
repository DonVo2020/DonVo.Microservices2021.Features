using System;
using DonVo.SignalR_MediatR.Core.Helpers;
using DonVo.SignalR_MediatR.Core.Models.Entities;

namespace DonVo.SignalR_MediatR.Core.Models.Responses
{
    public class ReminderResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Priority Priority { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}