using FluentValidation;
using ASP34.Contracts.Models.Rooms;

namespace ASP34.Contracts.Validation
{
    /// <summary>
    /// Класс-валидатор запросов на добавление новой комнаты
    /// </summary>
    public class AddRoomRequestValidator : AbstractValidator<AddRoomRequest>
    {
        public AddRoomRequestValidator() 
        {
            RuleFor(x => x.Area).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().Must(BeSupported).WithMessage($"Please choose one of the following locations: {string.Join(", ", Values.ValidRooms)}");
            RuleFor(x => x.Voltage).NotEmpty();
            RuleFor(x => x.GasConnected).NotNull();
        }

        private bool BeSupported(string location)
        {
            return Values.ValidRooms.Any(e => e == location);
        }
    }
}