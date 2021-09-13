using FluentValidation;

namespace DonVo.SignalR_MediatR.Core.Models.Requests.Validators
{
    public class AddSubjectGradeRequestValidator : AbstractValidator<AddGradeRequest>
    {
        public AddSubjectGradeRequestValidator()
        {
            RuleFor(x => x.Grade).MaximumLength(3);
            RuleFor(x => x.DateSet).NotEmpty();
        }
    }
}
