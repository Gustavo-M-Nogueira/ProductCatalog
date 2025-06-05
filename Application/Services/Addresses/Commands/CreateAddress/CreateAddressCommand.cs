using Application.DTOs.Requests;
using Application.DTOs.Responses;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Application.Services.Addresses.Commands.CreateAddress
{
    public record CreateAddressCommand(
        AddressRequestDto Address) 
        : ICommand<CreateAddressResult>;

    public record CreateAddressResult(AddressResponseDto Address);

    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(x => x.Address.AddressLine).NotEmpty().WithMessage("Address line cannot be empty");
            RuleFor(x => x.Address.Country).NotEmpty().WithMessage("Country cannot be empty");
            RuleFor(x => x.Address.State).NotEmpty().WithMessage("State cannot be empty");
            RuleFor(x => x.Address.ZipCode).NotEmpty().WithMessage("Zip code cannot be empty");
        }
    }
}
