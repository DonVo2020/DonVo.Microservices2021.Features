namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Params
{
    public class DepartmentParams
    {
        public string FilterBy { get; set; }
        public string OrderBy { get; set; } = "priority";
    }
}
