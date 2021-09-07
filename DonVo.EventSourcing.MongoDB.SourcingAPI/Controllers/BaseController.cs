using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace DonVo.EventSourcing.MongoDB.SourcingAPI.Controllers
{
    [Route("api/v1/[controller]/[action]"), ApiController, Produces(MediaTypeNames.Application.Json)]
    public class BaseController : ControllerBase
    {
    }
}
