using UberEats.Core.Application.Interfaces.Repositories;
using UberEats.Core.Application.Interfaces.Services;
using UberEats.Core.Domain.Entities;

namespace UberEats.Core.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            await _categoryRepository.DeleteCategoryAsync(categoryId);
        }

        public async Task<ICollection<Category>> GetAllCategoriesAsync()
        {
            var result = await _categoryRepository.GetAllCategoriesAsync();
            return result;
        }

        public Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            var result = _categoryRepository.GetCategoryByIdAsync(categoryId);
            return result;
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _categoryRepository.UpdateCategoryAsync(category);
        }
    }
}
