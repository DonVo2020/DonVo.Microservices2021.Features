using System;

namespace DonVo.SignalR_MediatR.Core.Models.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}