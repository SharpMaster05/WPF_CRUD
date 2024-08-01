using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using WPF_CRUD.Infrastucture;
using WPF_CRUD.Views.Pages;

namespace WPF_CRUD.ViewModels.Windows;

internal class MainViewModel : Notifier
{
    public Navigation Navigation { get; set; }
    public MainViewModel(Navigation navigation)
    {
        Navigation = navigation;
    }

    public ICommand CloseCommand => new Command(x => Animation.CloseAnimation(x as Border, "MainWindow"));
    public ICommand MaximizeCommand => new Command(x => Animation.Maximize(x as Border));
    public ICommand MinimizeCommand => new Command(x => Animation.Minimize(x as Border));
    public ICommand NavigateToProductViewCommand => new Command(x => Navigation.ChangePage(new ProductView()));
    public ICommand NavigateToCategoryViewCommand => new Command(x => Navigation.ChangePage(new CategoryView()));

    public ICommand OpenCommand => new Command(x =>
    {
        var border = x as Border;
        var time = TimeSpan.FromSeconds(0.5);

        DoubleAnimation open = new(0, 800, time)
        {
            FillBehavior = FillBehavior.Stop,
            EasingFunction = new PowerEase { Power = 2, EasingMode = EasingMode.EaseOut }
        };

        open.Completed += (s, e) => Navigation.ChangePage(new CategoryView());

        border.BeginAnimation(FrameworkElement.WidthProperty, open);
    });

}
