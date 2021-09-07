using System;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses
{
    public class CustomRequestDetailsListResponse
    {
        public string Category { get; set; }
        public string Description { get; set; }
        public string ItemId { get; set; }
        public string Qty { get; set; }
    }
}