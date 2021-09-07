using FluentValidation;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Requests.Validators
{
    public class AddDepartmentRequestValidator : AbstractValidator<AddDepartmentRequest>
    {
        public AddDepartmentRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.CollectionPointId).NotNull();
        }
    }
}
