using BLL.Abstractions;
using System.Collections.ObjectModel;
using WPF_CRUD.Infrastucture;

namespace WPF_CRUD.ViewModels.Abstractions;

internal class BaseViewModel<T> : Notifier where T : class, new()
{
    public ObservableCollection<T> Items { get; set; }
    public T SelectedItem { get; set; }
    public async Task InitializeAsync(IService<T> service)
    {
        var items = await service.GetAll();
        Items = new(items);
    }
}
