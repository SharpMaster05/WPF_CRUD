using BLL.Dto;
using BLL.Services;
using System.Windows.Input;
using WPF_CRUD.Infrastucture;
using WPF_CRUD.ViewModels.Abstractions;

namespace WPF_CRUD.ViewModels.Pages;

internal class ProductViewModel : BaseViewModel<ProductDto>
{
    private readonly ProductService _productService;
    private readonly CategoryService _categoryService;
    public ProductViewModel(ProductService productService, CategoryService categoryService) : base(productService)
    {
        _productService = productService;
        _categoryService = categoryService;
        Item = new();
    }

    public List<string> Categories { get; set; }
    public string SelectedCategory { get; set; }

    public ICommand InitializeCategoriesCommand => new Command(async x =>
    {
        var categories = await _categoryService.GetAll();
        Categories = new(categories.Select(x => x.CategoryName));
    });

    public ICommand CategoryId => new Command(async x =>
    {
        var categoryId = (await _categoryService.GetAll()).FirstOrDefault(x => x.CategoryName == SelectedCategory).Id;
        Item.CategoryId = categoryId;
    });
}
