namespace DonVo.SignalR_MediatR.Core.Models.Params
{
    public class ReminderParams
    {
        public string FilterBy { get; set; }
        public string OrderBy { get; set; } = "priority";
    }
}
