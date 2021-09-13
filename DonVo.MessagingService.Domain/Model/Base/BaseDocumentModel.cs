using MongoDB.Bson;
using System;

namespace DonVo.MessagingService.Domain.Model.Base
{
    public abstract class BaseDocumentModel
    {
        public ObjectId Id { get; set; }
        public DateTime InsertAt { get; set; }
    }
}
