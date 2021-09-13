using DonVo.MessagingService.Domain.Model.Base;

namespace DonVo.MessagingService.Domain.Model
{
    public class AccountModel : BaseDocumentModel
    {
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string EMail { get; set; }
        public System.DateTime LastLogin { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public System.Collections.Generic.List<string> BlockedUsers { get; set; }
    }
}
