using ProductCatalog.API.Data.Repositories;
using ProductCatalog.API.Models.Entities;

namespace ProductCatalog.API.GraphQL
{
    public class Query
    {
        private readonly AddressRepository _addressRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly ProductRepository _productRepository;
        private readonly SupplierRepository _supplierRepository;
        private readonly TagRepository _tagRepository;

        public Query(
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
        public async Task<IEnumerable<Address>> GetAddresses()
        {
            IEnumerable<Address> addresses = await _addressRepository.GetAll();

            return addresses;
        }

        public async Task<Address> GetAddressById(Guid id)
        {
            Address? address = await _addressRepository.Find(id);

            if (address is null)
                throw new GraphQLException(new Error("Address not found", "ADDRESS_NOT_FOUND"));

            return address;
        }


        // Category
        public async Task<IEnumerable<Category>> GetCategories()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetAll();

            return categories;
        }

        public async Task<Category> GetCategoryById(int id)
        {
            Category? category = await _categoryRepository.Find(id);

            if (category is null)
                throw new GraphQLException(new Error("Category not found", "CATEGORY_NOT_FOUND"));

            return category;
        }


        // Product
        public async Task<IEnumerable<Product>> GetProducts()
        {
            IEnumerable<Product> products = await _productRepository.GetAll();

            return products;
        }

        public async Task<Product> GetProductById(Guid id)
        {
            Product? product = await _productRepository.Find(id);

            if (product is null)
                throw new GraphQLException(new Error("Product not found", "PRODUCT_NOT_FOUND"));

            return product;
        }


        // Supplier
        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            IEnumerable<Supplier> suppliers = await _supplierRepository.GetAll();

            return suppliers;
        }

        public async Task<Supplier> GetSupplierById(Guid id)
        {
            Supplier? supplier = await _supplierRepository.Find(id);

            if (supplier is null)
                throw new GraphQLException(new Error("Supplier not found", "SUPPLIER_NOT_FOUND"));

            return supplier;
        }


        // Tag
        public async Task<IEnumerable<Models.Entities.Tag>> GetTags()
        {
            IEnumerable<Models.Entities.Tag> tags = await _tagRepository.GetAll();

            return tags;
        }

        public async Task<Models.Entities.Tag> GetTagById(int id)
        {
            Models.Entities.Tag? tag = await _tagRepository.Find(id);

            if (tag is null)
                throw new GraphQLException(new Error("Tag not found", "TAG_NOT_FOUND"));

            return tag;
        }
    }
}
