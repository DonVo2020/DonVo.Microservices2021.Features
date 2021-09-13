using DonVo.MessagingService.Domain.Model.Base;

namespace DonVo.MessagingService.Domain.Model
{
    public class MessageModel : BaseDocumentModel
    {
        public string SenderUser { get; set; }
        public string ReceiverUser { get; set; }
        public string Message { get; set; }
    }
}
