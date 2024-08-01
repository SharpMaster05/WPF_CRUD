using BLL.Abstractions;
using BLL.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WPF_CRUD.Infrastucture;

namespace WPF_CRUD.ViewModels.Abstractions;

internal class BaseViewModel<T> : Notifier where T : class, new()
{
    private readonly IService<T> _service;

    public BaseViewModel(IService<T> service)
    {
        _service = service;
    }

    public ObservableCollection<T> Items { get; set; }
    public T SelectedItem { get; set; }
    public async Task InitializeAsync(IService<T> service)
    {
        var items = await service.GetAll();
        Items = new(items);
    }

    public ICommand InitializeCommand => new Command(async x => await InitializeAsync(_service));
}
