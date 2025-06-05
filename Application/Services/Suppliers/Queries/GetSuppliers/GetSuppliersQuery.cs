using Application.DTOs.Responses;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Application.Services.Suppliers.Queries.GetSuppliers
{
    public record GetSuppliersQuery() : IQuery<GetSuppliersResult>;
    public record GetSuppliersResult(IEnumerable<SupplierResponseDto> Suppliers);
    public class GetSuppliersQueryValidator : AbstractValidator<GetSuppliersQuery>
    {
        public GetSuppliersQueryValidator()
        {
            
        }
    }
}
