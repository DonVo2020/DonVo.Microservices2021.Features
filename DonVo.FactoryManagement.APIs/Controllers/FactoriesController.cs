using DonVo.FactoryManagement.Models.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace DonVo.FactoryManagement.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactoriesController : ControllerBase
    {
        private readonly FactoryManagementContext _context;

        public FactoriesController(FactoryManagementContext context)
        {
            _context = context;
        }
    }
}
