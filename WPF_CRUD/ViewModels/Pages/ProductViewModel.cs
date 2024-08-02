using BLL.Dto;
using BLL.Services;
using System.Windows.Input;
using WPF_CRUD.Infrastucture;
using WPF_CRUD.ViewModels.Abstractions;

namespace WPF_CRUD.ViewModels.Pages;

internal class ProductViewModel : BaseViewModel<ProductDto>
{
    private readonly ProductService _productService;
    public ProductViewModel(ProductService productService) : base(productService)
    {
        _productService = productService;
        Item = new();
    }
}
