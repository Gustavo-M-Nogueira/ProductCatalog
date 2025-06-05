using Application.DTOs.Requests;
using Application.DTOs.Responses;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Application.Services.Addresses.Commands.UpdateAddress
{
    public record UpdateAddressCommand
        (Guid Id, AddressRequestDto Address) 
        : ICommand<UpdateAddressResult>;
    public record UpdateAddressResult(AddressResponseDto Address);
    public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
    {
        public UpdateAddressCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
            RuleFor(x => x.Address.AddressLine).NotEmpty().WithMessage("Address Line cannot be empty");
            RuleFor(x => x.Address.Country).NotEmpty().WithMessage("Country cannot be empty");
            RuleFor(x => x.Address.State).NotEmpty().WithMessage("State cannot be empty");
            RuleFor(x => x.Address.ZipCode).NotEmpty().WithMessage("Zip code cannot be empty");
        }
    }
}
