using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using BLL.Dto;
using BLL.Services;
using WPF_CRUD.Infrastucture;
using WPF_CRUD.ViewModels.Abstractions;

namespace WPF_CRUD.ViewModels.Pages;

internal class ProductViewModel : BaseViewModel<ProductDto>
{
    private readonly ProductService _productService;
    private readonly CategoryService _categoryService;

    public ProductViewModel(ProductService productService, CategoryService categoryService)
        : base(productService)
    {
        _productService = productService;
        _categoryService = categoryService;
        Item = new();
    }

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
            var categoryId = (await _categoryService.GetAll())
                .FirstOrDefault(x => x.CategoryName == SelectedCategory)
                .Id;
            Item.CategoryId = categoryId;
        });

    public new ICommand SelectItemCommand =>
        new Command(async x =>
        {
            SelectedItem = x as ProductDto;
            Item = SelectedItem;
            SelectedCategory = (await _categoryService.GetAll())
                .FirstOrDefault(x => x.Id == SelectedItem.CategoryId)
                .CategoryName;

            if(!string.IsNullOrEmpty(SelectedItem.ImagePath))
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
}
