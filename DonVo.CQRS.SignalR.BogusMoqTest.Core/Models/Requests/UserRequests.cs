namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Requests
{
    public record LoginRequest(string Email, string Password);
    public record RegisterRequest(string Email, string Password);
}
