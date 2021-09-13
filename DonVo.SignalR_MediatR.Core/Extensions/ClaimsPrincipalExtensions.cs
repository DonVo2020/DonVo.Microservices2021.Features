using DonVo.SignalR_MediatR.Core.Repositories;
using System.Security.Claims;

namespace DonVo.SignalR_MediatR.Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {

        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "3e297024-631c-4903-b939-5513206786c8";
        }

        public static string GetUserEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;
        }
    }
}
