using System;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}