using BLL.Dto;
using BLL.Services;
using System.Windows.Input;
using WPF_CRUD.Infrastucture;
using WPF_CRUD.ViewModels.Abstractions;
using WPF_CRUD.Views.Pages;

namespace WPF_CRUD.ViewModels.Pages
{
    internal class CategoryViewModel : BaseViewModel<CategoryDto>
    {
        private readonly CategoryService _categoryService;
        private readonly ProductService _productService;
        private readonly Navigation _navigation;
        public CategoryViewModel(CategoryService categoryService, ProductService productService, Navigation navigation) : base(categoryService) 
        {
            _categoryService = categoryService;
            _productService = productService;
            _navigation = navigation;
            Item = new();
        }

        public ICommand NavigateToProductsCommand => new Command(async x => 
        {
            SelectedItem = x as CategoryDto;
            var products = (await _productService.GetAll()).Where(x => x.CategoryId == SelectedItem.Id);
            var view = new ProductsFromCategoryView();
            var viewModel = new ProductsFromCategotyViewModel(_productService, _categoryService, products, SelectedItem);
           
            view.DataContext = viewModel;

            _navigation.ChangePage(view);
        });
    }
}
