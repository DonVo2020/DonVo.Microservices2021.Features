namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses
{
    public class DepartmentResponse
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int CollectionPointId { get; set; }
        public int? DepartmentHeadId { get; set; }
        public int? DepartmentRepId { get; set; }
    }
}