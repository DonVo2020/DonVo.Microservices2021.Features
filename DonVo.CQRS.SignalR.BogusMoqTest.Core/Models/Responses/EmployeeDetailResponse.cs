using System.Collections.Generic;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses
{
    public class EmployeeDetailResponse
    {
        public string DepartmentId { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
