using FluentValidation;
using RentalSystem.Api.Dto.Reservations;

namespace RentalSystem.Api.DtoValidation
{
    public class CustomerCarReservationValidator : AbstractValidator<CarReservationDto>
    {
        public CustomerCarReservationValidator()
        {
            RuleFor(x => x.CarId).NotEmpty()
                .NotEmpty()
                .WithMessage("Car id is not provided.");
            RuleFor(x => x.RentFrom).Must(date => date != default);
            RuleFor(x => x.RentTo).Must(date => date != default);
        }
    }
}
