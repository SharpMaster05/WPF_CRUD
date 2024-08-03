using System.Windows.Input;
using BLL.Dto;
using BLL.Services;
using WPF_CRUD.Infrastucture;
using WPF_CRUD.ViewModels.Abstractions;

namespace WPF_CRUD.ViewModels.Pages;

internal class ProductsFromCategotyViewModel : BaseViewModel<ProductDto>
{
    private readonly ProductService _productService;
    private readonly CategoryService _categoryService;

    public ProductsFromCategotyViewModel(
        ProductService productService,
        CategoryService categoryService,
        IEnumerable<ProductDto> products
    )
        : base(productService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    public ICommand InitializeCategoriesCommand =>
        new Command(async x =>
        {
            var categories = await _categoryService.GetAll();
            Categories = new(categories.Select(x => x.CategoryName));
        });

    public ICommand CategoryId =>
        new Command(async x =>
        {
            var categoryId = (await _categoryService.GetAll())
                .FirstOrDefault(x => x.CategoryName == SelectedCategory)
                .Id;
            Item.CategoryId = categoryId;
        });
}
