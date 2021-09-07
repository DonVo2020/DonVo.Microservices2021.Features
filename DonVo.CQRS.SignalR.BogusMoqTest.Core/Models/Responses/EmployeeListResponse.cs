namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses
{
    public class EmployeeListResponse
    {
        public int Id { get; set; }
        public string DepartmentId { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActingDepartmentHead { get; set; }
    }
}
