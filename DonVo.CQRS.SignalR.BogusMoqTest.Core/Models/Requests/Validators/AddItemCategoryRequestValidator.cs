using FluentValidation;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Requests.Validators
{
    public class AddItemCategoryRequestValidator : AbstractValidator<AddItemCategoryRequest>
    {
        public AddItemCategoryRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
