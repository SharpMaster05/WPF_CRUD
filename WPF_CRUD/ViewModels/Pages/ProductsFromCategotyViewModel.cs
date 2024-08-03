using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using BLL.Dto;
using BLL.Services;
using DAL.Models;
using WPF_CRUD.Infrastucture;
using WPF_CRUD.ViewModels.Abstractions;

namespace WPF_CRUD.ViewModels.Pages;

internal class ProductsFromCategotyViewModel : BaseViewModel<ProductDto>
{
    private readonly ProductService _productService;
    private readonly CategoryService _categoryService;
    private readonly CategoryDto _category;
    public ProductsFromCategotyViewModel(
        ProductService productService,
        CategoryService categoryService,
        IEnumerable<ProductDto> products,
        CategoryDto category
    )
        : base(productService)
    {
        _productService = productService;
        _categoryService = categoryService;
        _category = category;
        Products = new(products);
        Item = new();
        ItemsCount = Products.Count;
    }

    private async Task Reload()
    {
        var products = (await _productService.GetAll()).Where(x => x.CategoryId == _category.Id);
        Products = new(products);
        ItemsCount = Products.Count;
        SelectedItem = null;
        Item = new();
        SelectedCategory = null;
        Image = new();
    }

    public ObservableCollection<ProductDto> Products { get; set; }
    public Image Image { get; set; } = new Image();

    public ICommand InitializeCategoriesCommand =>
        new Command(async x =>
        {
            var categories = await _categoryService.GetAll();
            Categories = new(categories.Select(x => x.CategoryName));
        });

    public ICommand CategoryId =>
        new Command(async x =>
        {
            if(SelectedCategory != null)
            {
                var categoryId = (await _categoryService.GetAll())
                    .FirstOrDefault(x => x.CategoryName == SelectedCategory)
                    .Id;
                Item.CategoryId = categoryId;
            }
        });

    public new ICommand SelectItemCommand =>
        new Command(async x =>
        {
            SelectedItem = x as ProductDto;
            Item = SelectedItem;
            SelectedCategory = (await _categoryService.GetAll())
                .FirstOrDefault(x => x.Id == SelectedItem.CategoryId)
                .CategoryName;

            if (!string.IsNullOrEmpty(SelectedItem.ImagePath))
            {
                Image = new Image() { Source = new BitmapImage(new Uri(SelectedItem.ImagePath)) };
                Item.ImagePath = SelectedItem.ImagePath;
            }
        });

    public ICommand SelectImageCommand =>
        new Command(x =>
        {
            using (var file = new OpenFileDialog())
            {
                if (file.ShowDialog() == DialogResult.OK)
                {
                    file.Filter =
                        "Jpg files (*.jpg)|*.jpg|"
                        + "Png files (*.png)|*.png|"
                        + "Jpeg files (*.jpeg)|*.jpeg";

                    Item.ImagePath = file.FileName;
                    Image = new Image() { Source = new BitmapImage(new Uri(file.FileName)) };
                }
            }
        });

    public new ICommand AddCommand => new Command(async x =>
    {
        Products.Add(Item);
        await _productService.Add(Item);
        await Reload();
        Item = new();
    });

    public new ICommand UpdateCommand => new Command(async x =>
    {
        await _productService.Update(Item);
        Item = new();
    }, x => SelectedItem != null);

    public new ICommand DeleteCommand => new Command(async x =>
    {
        await _productService.Delete(Item);
        await Reload();
        Item = new();
    }, x=> SelectedItem != null);

    public ICommand ReloadCommand => new Command(async x => await Reload());
}
