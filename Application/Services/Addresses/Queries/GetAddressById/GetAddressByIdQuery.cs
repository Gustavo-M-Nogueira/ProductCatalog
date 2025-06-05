using Application.DTOs.Responses;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Application.Services.Addresses.Queries.GetAddressById
{
    public record GetAddressByIdQuery(Guid Id) : IQuery<GetAddressByIdResult>;
    public record GetAddressByIdResult(AddressResponseDto Address);
    public class GetAddressByIdQueryValidator : AbstractValidator<GetAddressByIdQuery>
    {
        public GetAddressByIdQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required.");
        }
    }
}
