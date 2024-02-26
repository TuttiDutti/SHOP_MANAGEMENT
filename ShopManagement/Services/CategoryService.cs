using ShopManagement.DAL;
using ShopManagement.Models;

namespace ShopManagement.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository _categoryRepository;
        public CategoryService(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Category? GetById(int id)
        {
            Category? category = _categoryRepository.GetById(id);
            return category;
        }

        public List<Category> GetAll()
        {
            List<Category> categorys = _categoryRepository.GetAll().ToList();
            return categorys;
        }

        public Category Add(Category category)
        {
            Category? newCategory = _categoryRepository.AddAndSaveChanges(category);

            return newCategory;
        }

        public void Update(Category category)
        {
            _categoryRepository.UpdateAndSaveChanges(category);
        }

        public void Delete(int id)
        {
            Category? category = _categoryRepository.GetById(id);

            _categoryRepository.RemoveByIdAndSaveChanges(id);
        }
    }
}
