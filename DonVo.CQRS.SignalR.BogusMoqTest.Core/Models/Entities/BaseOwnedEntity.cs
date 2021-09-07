using System;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Entities
{
    public abstract class BaseOwnedEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
