using ASP34.Contracts.Models.Rooms;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP34.Contracts.Validation
{
    public class EditRoomRequestValidator : AbstractValidator<EditRoomRequest>
    {
        public EditRoomRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Must(BeSupported).WithMessage($"Please choose one of the following locations: {string.Join(", ", Values.ValidRooms)}");
            RuleFor(x => x.Area).NotEmpty().NotNull();
            RuleFor(x => x.Voltage).NotEmpty().NotNull();
            RuleFor(x => x.GasConnected).NotNull();
        }

        private bool BeSupported(string location)
        {
            return Values.ValidRooms.Any(e => e == location);
        }
    }
}
