using FluentValidation;

namespace DonVo.EventSourcing.Ordering.Application.Commands.OrderCreate
{
    public class OrderCreateValidator : AbstractValidator<OrderCreateCommand>
    {
        public OrderCreateValidator()
        {
            RuleFor(v => v.SellerUserName)
                .EmailAddress()
                .NotNull()
                .NotEmpty();

            RuleFor(v => v.ProductId)
                .NotNull()
                .NotEmpty();
        }
    }
}
