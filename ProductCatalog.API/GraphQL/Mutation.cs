using Mapster;
using ProductCatalog.API.Data.Repositories;
using ProductCatalog.API.Models.DTOs;
using ProductCatalog.API.Models.Entities;

namespace ProductCatalog.API.GraphQL
{
    public class Mutation
    {
        private readonly AddressRepository _addressRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly ProductRepository _productRepository;
        private readonly SupplierRepository _supplierRepository;
        private readonly TagRepository _tagRepository;

        public Mutation(
            AddressRepository addressRepository, 
            CategoryRepository categoryRepository, 
            ProductRepository productRepository, 
            SupplierRepository supplierRepository, 
            TagRepository tagRepository)
        {
            _addressRepository = addressRepository;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
            _tagRepository = tagRepository;
        }

        // Address
        public async Task<Address> CreateAddress(AddressRequest request)
        {
            Address address = request.Adapt<Address>();

            var response = await _addressRepository.Create(address);

            return response;
        }
        
        public async Task<Address> UpdateAddress(Guid id, AddressRequest request)
        {
            Address? address = await _addressRepository.Find(id);

            if (address is null)
                throw new GraphQLException(new Error("Address not found", "ADDRESS_NOT_FOUND"));

            address.AddressLine = request.AddressLine;
            address.Country = request.Country;
            address.State = request.State;
            address.ZipCode = request.ZipCode;

            var response = await _addressRepository.Update(address);

            return response;
        }

        public async Task<bool> DeleteAddress(Guid id)
        {
            Address? address = await _addressRepository.Find(id);

            if (address is null)
                throw new GraphQLException(new Error("Address not found", "ADDRESS_NOT_FOUND"));

            var response = await _addressRepository.Delete(id);

            return response;
        }


        // Category
        public async Task<Category> CreateCategory(CategoryRequest request)
        {
            Category category = request.Adapt<Category>();


            var response = await _categoryRepository.Create(category);

            return response;
        }

        public async Task<Category> UpdateCategory(int id, CategoryRequest request)
        {
            Category? category = await _categoryRepository.Find(id);

            if (category is null)
                throw new GraphQLException(new Error("Category not found", "CATEGORY_NOT_FOUND"));

            category.Title = request.Title;

            var response = await _categoryRepository.Update(category);

            return response;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            Category? category = await _categoryRepository.Find(id);

            if (category is null)
                throw new GraphQLException(new Error("Category not found", "CATEGORY_NOT_FOUND"));

            var response = await _categoryRepository.Delete(id);

            return response;
        }


        // Product
        public async Task<Product> CreateProduct(ProductRequest request)
        {
            Product product = request.Adapt<Product>();

            var response = await _productRepository.Create(product);

            return response;
        }

        public async Task<Product> UpdateProduct(Guid id, ProductRequest request)
        {
            Product? product = await _productRepository.Find(id);

            if (product is null)
                throw new GraphQLException(new Error("Product not found", "PRODUCT_NOT_FOUND"));

            product.Title = request.Title;
            product.Price = request.Price;
            product.Description = request.Description;
            product.StockQuantity = request.StockQuantity;
            product.CategoryId = request.CategoryId;
            product.SupplierId = request.SupplierId;

            var response = await _productRepository.Update(product);

            return response;
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            Product? product = await _productRepository.Find(id);

            if (product is null)
                throw new GraphQLException(new Error("Product not found", "PRODUCT_NOT_FOUND"));

            var response = await _productRepository.Delete(id);

            return response;
        }


        // Supplier
        public async Task<Supplier> CreateSupplier(SupplierRequest request)
        {
            Supplier supplier = request.Adapt<Supplier>();

            var response = await _supplierRepository.Create(supplier);

            return response;
        }

        public async Task<Supplier> UpdateSupplier(Guid id, SupplierRequest request)
        {
            Supplier? supplier = await _supplierRepository.Find(id);

            if (supplier is null)
                throw new GraphQLException(new Error("Supplier not found", "SUPPLIER_NOT_FOUND"));

            supplier.Name = request.Name;
            supplier.Description = request.Description;
            supplier.AddressId = request.AddressId;

            var response = await _supplierRepository.Update(supplier);

            return response;
        }

        public async Task<bool> DeleteSupplier(Guid id)
        {
            Supplier? supplier = await _supplierRepository.Find(id);

            if (supplier is null)
                throw new GraphQLException(new Error("Supplier not found", "SUPPLIER_NOT_FOUND"));

            var response = await _supplierRepository.Delete(id);

            return response;
        }


        // Tag
        public async Task<Models.Entities.Tag> CreateTag(TagRequest request)
        {
            Models.Entities.Tag tag = request.Adapt<Models.Entities.Tag>();

            var response = await _tagRepository.Create(tag);

            return response;
        }

        public async Task<Models.Entities.Tag> UpdateTag(int id, TagRequest request)
        {
            Models.Entities.Tag? tag = await _tagRepository.Find(id);

            if (tag is null)
                throw new GraphQLException(new Error("Tag not found", "TAG_NOT_FOUND"));

            tag.Title = request.Title;

            var response = await _tagRepository.Update(tag);

            return response;
        }

        public async Task<bool> DeleteTag(int id)
        {
            Models.Entities.Tag? tag = await _tagRepository.Find(id);

            if (tag is null)
                throw new GraphQLException(new Error("Tag not found", "TAG_NOT_FOUND"));

            var response = await _tagRepository.Delete(id);

            return response;
        }
    }
}
