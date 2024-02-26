using ShopManagement.DAL;
using ShopManagement.Models;

namespace ShopManagement.Services
{
    public class ProductService
    {
        private readonly ProductRepository _productRepository;
        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product? GetById(int id)
        {
            Product? product = _productRepository.GetById(id);
            return product;
        }

        public List<Product> GetAll()
        {
            List<Product> products = _productRepository.GetAll().ToList();
            return products;
        }

        public List<Product> GetNotBLocked()
        {
            List<Product> products =_productRepository.GetAll()
                .Where(u => u.IsBlocked == false)
                .ToList();
            return products;
        }

        public Product Add(Product product)
        {
            Product? newProduct = _productRepository.AddAndSaveChanges(product);

            return newProduct;
        }

        public void Update(Product product)
        {
            _productRepository.UpdateAndSaveChanges(product);
        }

        public void Delete(int id)
        {
            Product? product = _productRepository.GetById(id);

            _productRepository.RemoveByIdAndSaveChanges(id);
        }

        public List<Product> GetByCategory(int categoryId)
        {
            List<Product> products = _productRepository.GetAll()
                .Where(a => a.CategoryId == categoryId).ToList();
            return products;
        }
    }
}
