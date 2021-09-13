using DonVo.MessagingService.Domain.Model;
using DonVo.MessagingService.Domain.Repositories.Base;
using System.Collections.Generic;

namespace DonVo.MessagingService.Domain.Repositories
{
    public interface IMessageRepository : IGenericRepository<MessageModel>
    {
        MessageModel GetSingleMessage(string id, string username);
        List<string> GetOlderMessages(string id, string username);
        string GetLatestMessage(string senderUsername, string receiverUsername);
    }

}
