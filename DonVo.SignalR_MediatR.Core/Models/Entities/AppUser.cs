using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DonVo.SignalR_MediatR.Core.Models.Entities
{
    public class AppUser : IdentityUser
    {
        public ICollection<Message> MessagesRecieved { get; set; } = new List<Message>();
        public ICollection<Message> MessagesSent { get; set; } = new List<Message>();
    }
}
