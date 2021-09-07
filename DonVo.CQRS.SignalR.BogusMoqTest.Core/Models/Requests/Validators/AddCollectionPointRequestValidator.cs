using FluentValidation;

namespace DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Requests.Validators
{
    public class AddCollectionPointRequestValidator : AbstractValidator<AddCollectionPointRequest>
    {
        public AddCollectionPointRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Time).NotEmpty();
        }
    }
}
