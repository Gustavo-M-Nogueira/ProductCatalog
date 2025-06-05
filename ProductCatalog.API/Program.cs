using Domain.Entities;
using ProductCatalog.API.GraphQL.Mutations;
using ProductCatalog.API.GraphQL.Queries;
using Application;
using Infrastructure;
using ProductCatalog.API.GraphQL.Mutations.Address;
using ProductCatalog.API.GraphQL.Mutations.Category;
using ProductCatalog.API.GraphQL.Mutations.Product;
using ProductCatalog.API.GraphQL.Mutations.Supplier;
using ProductCatalog.API.GraphQL.Mutations.Tag;
using ProductCatalog.API.GraphQL.Queries.Address;
using ProductCatalog.API.GraphQL.Queries.Category;
using ProductCatalog.API.GraphQL.Queries.Product;
using ProductCatalog.API.GraphQL.Queries.Supplier;
using ProductCatalog.API.GraphQL.Queries.Tag;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

builder.Services.AddGraphQLServer()
    .RegisterDbContextFactory<ApplicationDbContext>()

    .AddQueryType<Query>()

    .AddTypeExtension<GetAddressesGraphQLQuery>()
    .AddTypeExtension<GetAddressByIdGraphQLQuery>()

    .AddTypeExtension<GetCategoriesGraphQLQuery>()
    .AddTypeExtension<GetCategoryByIdGraphQlQuery>()

    .AddTypeExtension<GetProductsGraphQLQuery>()
    .AddTypeExtension<GetProductByIdGraphQlQuery>()

    .AddTypeExtension<GetSuppliersGraphQlQuery>()
    .AddTypeExtension<GetSupplierByIdGraphQLQuery>()
    
    .AddTypeExtension<GetTagsGraphQlQuery>()
    .AddTypeExtension<GetTagByIdGraphQLQuery>()

    .AddMutationType<Mutation>()
    
    .AddTypeExtension<CreateAddressMutation>()
    .AddTypeExtension<DeleteAddressMutation>()
    .AddTypeExtension<UpdateAddressMutation>()

    .AddTypeExtension<CreateCategoryMutation>()
    .AddTypeExtension<DeleteCategoryMutation>()
    .AddTypeExtension<UpdateCategoryMutation>()

    .AddTypeExtension<CreateProductMutation>()
    .AddTypeExtension<DeleteProductMutation>()
    .AddTypeExtension<UpdateProductMutation>()

    .AddTypeExtension<CreateSupplierMutation>()
    .AddTypeExtension<DeleteSupplierMutation>()
    .AddTypeExtension<UpdateSupplierMutation>()

    .AddTypeExtension<CreateTagMutation>()
    .AddTypeExtension<DeleteTagMutation>()
    .AddTypeExtension<UpdateTagMutation>();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL("/graphql");
});

app.Run();
