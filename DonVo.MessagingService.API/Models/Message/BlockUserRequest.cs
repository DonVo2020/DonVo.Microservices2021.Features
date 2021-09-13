using FluentValidation;
using DonVo.MessagingService.API.Helpers;
using System.Collections.Generic;

namespace DonVo.MessagingService.API.Models.Message
{
    public class BlockUserRequest
    {
        public string Opponent { get; set; }
        public KeyValuePair<bool, string> IsValid() => new BlockUserRequestValidator().IsValid(this);
    }
    public class BlockUserRequestValidator : AbstractValidator<BlockUserRequest>
    {
        public BlockUserRequestValidator()
        {
            RuleFor(x => x.Opponent).MinimumLength(4).MaximumLength(12);
        }
    }
}
