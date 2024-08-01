using BLL.Dto;
using BLL.Services;
using System.Windows.Input;
using WPF_CRUD.Infrastucture;
using WPF_CRUD.ViewModels.Abstractions;

namespace WPF_CRUD.ViewModels.Pages
{
    internal class CategoryViewModel : BaseViewModel<CategoryDto>
    {
        private readonly CategoryService _categoryService;
        public CategoryViewModel(CategoryService categoryService) : base(categoryService) 
        {
            _categoryService = categoryService;
        }
    }
}
