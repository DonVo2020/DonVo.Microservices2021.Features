namespace DonVo.MessagingService.API.Models.Common
{
    public class BadRequestResponse
    {
        public string Error { get; set; }

        public BadRequestResponse(string error)
        {
            Error = error;
        }
    }
}
