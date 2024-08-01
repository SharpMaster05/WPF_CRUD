using System.Windows;
using WPF_CRUD.Infrastucture;

namespace WPF_CRUD;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        DiContainer.Init();
        base.OnStartup(e);
    }
}
