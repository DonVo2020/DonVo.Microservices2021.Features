using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
