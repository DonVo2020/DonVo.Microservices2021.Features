using System;

namespace DonVo.SignalR_MediatR.Core.Models.Entities
{
    public abstract class BaseOwnedEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
