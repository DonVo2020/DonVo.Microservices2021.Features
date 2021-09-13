namespace DonVo.SignalR_MediatR.Core.Models.Responses
{
    public class SubjectDetailResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public GradeDetailResponse Grade { get; set; }
    }
}
