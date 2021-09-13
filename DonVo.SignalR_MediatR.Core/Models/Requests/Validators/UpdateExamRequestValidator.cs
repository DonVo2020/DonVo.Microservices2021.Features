using FluentValidation;

namespace DonVo.SignalR_MediatR.Core.Models.Requests.Validators
{
    public class UpdateExamRequestValidator : AbstractValidator<UpdateExamRequest>
    {
        public UpdateExamRequestValidator()
        {
            RuleFor(x => x.DueDate).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
