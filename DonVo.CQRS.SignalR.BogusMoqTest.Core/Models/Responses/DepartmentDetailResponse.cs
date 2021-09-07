using System.Collections.Generic;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses
{
    public class DepartmentDetailResponse
    {
        public string Name { get; set; }
        public int CollectionPointId { get; set; }
        public int? DepartmentHeadId { get; set; }
        public int? DepartmentRepId { get; set; }

        public ICollection<Employee> Employees { get; set; }
        public ICollection<Disbursement> Disbursements { get; set; }
    }
}
