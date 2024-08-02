﻿using BLL.Abstractions;
using BLL.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WPF_CRUD.Infrastucture;

namespace WPF_CRUD.ViewModels.Abstractions;

internal class BaseViewModel<T> : Notifier where T : class, new()
{
    private readonly IService<T> _service;

    public BaseViewModel(IService<T> service)
    {
        _service = service;

        CardView = Visibility.Visible;
        ListView = Visibility.Collapsed;
    }
    
    public async Task InitializeAsync(IService<T> service)
    {
        var items = await service.GetAll();
        Items = new(items);
        ItemsCount = Items.Count;
    }
    
    private void ChangeVisibility()
    {
        if(CardView == Visibility.Visible)
        {
            CardView = Visibility.Collapsed;
            ListView = Visibility.Visible;
        }
        else
        {
            CardView = Visibility.Visible;
            ListView = Visibility.Collapsed;
        }
    }

    public ObservableCollection<T> Items { get; set; }
    public T Item { get; set; }
    public T SelectedItem { get; set; }
    public Visibility CardView {  get; set; }
    public Visibility ListView {  get; set; }
    public int ItemsCount {  get; set; }

    public ICommand InitializeCommand => new Command(async x => await InitializeAsync(_service));
    public ICommand AddCommand => new Command(async x => 
    {
        await _service.Add(Item);
        await InitializeAsync(_service);
        Item = new();
    });
    public ICommand DeleteCommand => new Command(async x =>
    {
        await _service.Delete(SelectedItem);
        await InitializeAsync(_service);
        SelectedItem = null;
        Item = new();
    }, x => SelectedItem != null);

    public ICommand UpdateCommand => new Command(async x =>
    {
        await _service.Update(SelectedItem);
        SelectedItem = null;
        Item = new();
    }, x=> SelectedItem != null);

    public ICommand ChangeVisibilityCommand => new Command(x => ChangeVisibility());

    public ICommand SelectItemCommand => new Command(x =>
    {
        SelectedItem = x as T;
        Item = SelectedItem;
    });
}
