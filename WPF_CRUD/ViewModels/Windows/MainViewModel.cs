using BLL.Dto;
using BLL.Services;
using System.Windows.Input;
using WPF_CRUD.Infrastucture;
using WPF_CRUD.ViewModels.Abstractions;

namespace WPF_CRUD.ViewModels.Windows;

internal class MainViewModel : BaseViewModel<ProductDto>
{
    private readonly ProductService _productService;

    public MainViewModel(ProductService productService)
    {
        _productService = productService;
    }

    public ICommand InitializeCommand => new Command(async x => await InitializeAsync(_productService));
}
