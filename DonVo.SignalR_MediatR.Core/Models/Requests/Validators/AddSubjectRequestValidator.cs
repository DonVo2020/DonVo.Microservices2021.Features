using FluentValidation;

namespace DonVo.SignalR_MediatR.Core.Models.Requests.Validators
{
    public class AddSubjectRequestValidator : AbstractValidator<AddSubjectRequest>
    {
        public AddSubjectRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
