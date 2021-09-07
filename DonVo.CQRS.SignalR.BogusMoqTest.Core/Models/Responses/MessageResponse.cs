using System;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses
{
    public class MessageResponse
    {
        public int Id { get; set; }
        public string RecipientId { get; set; }
        public string SenderId { get; set; }
        public string SenderUsername { get; set; }
        public string RecipientUsername { get; set; }
        public string Content { get; set; }
        public DateTime DateSent { get; set; }
        public DateTime? DateRead { get; set; }
    }
}
