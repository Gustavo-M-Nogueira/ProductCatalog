using Application.DTOs.Responses;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Application.Services.Addresses.Queries.GetAddresses
{
    public record GetAddressesQuery() : IQuery<GetAddressesResult>;
    public record GetAddressesResult(IEnumerable<AddressResponseDto> Addresses);
    public class GetAddressesQueryValidator : AbstractValidator<GetAddressesQuery>
    {
        public GetAddressesQueryValidator()
        {
            
        }
    }
}
