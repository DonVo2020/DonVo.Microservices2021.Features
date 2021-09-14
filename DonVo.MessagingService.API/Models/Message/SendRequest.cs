using DonVo.MessagingService.API.Helpers;
using FluentValidation;
using System.Collections.Generic;

namespace DonVo.MessagingService.API.Models.Message
{
    public class SendRequest
    {
        public string ReceiverUser { get; set; }
        public string Message { get; set; }

        public KeyValuePair<bool, string> IsValid() => new SendRequestValidator().IsValid(this);   
    }

    public class SendRequestValidator : AbstractValidator<SendRequest>
    {
        public SendRequestValidator()
        {
            RuleFor(x => x.Message).NotEmpty();
            RuleFor(x => x.ReceiverUser).MinimumLength(4).MaximumLength(12);
        }
    }
}
