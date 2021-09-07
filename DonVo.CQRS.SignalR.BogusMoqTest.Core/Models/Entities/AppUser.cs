using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Message> MessagesRecieved { get; set; } = new List<Message>();
        public ICollection<Message> MessagesSent { get; set; } = new List<Message>();
    }
}
