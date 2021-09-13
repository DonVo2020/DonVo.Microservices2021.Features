using System;

namespace DonVo.SignalR_MediatR.Core.Models.Responses
{
    public class ExamListResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
    }
}