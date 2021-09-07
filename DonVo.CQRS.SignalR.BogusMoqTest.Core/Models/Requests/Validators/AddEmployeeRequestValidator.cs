using FluentValidation;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Requests.Validators
{
    public class AddEmployeeRequestValidator : AbstractValidator<AddEmployeeRequest>
    {
        public AddEmployeeRequestValidator()
        {
            //RuleFor(x => x.Priority).IsInEnum();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Role).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.DepartmentId).NotNull();
        }
    }
}
