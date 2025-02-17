﻿using BLL.Services;
using DAL.Abstractions;
using DAL.Context;
using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WPF_CRUD.ViewModels.Pages;
using WPF_CRUD.ViewModels.Windows;

namespace WPF_CRUD.Infrastucture;

internal class DiContainer
{
    private static IServiceProvider _provider;

    public static void Init()
    {
        var builder = new ServiceCollection();

        builder.AddAutoMapper(typeof(MappingProfile));
        builder.AddDbContext<AppDbContext>(options => options.UseSqlServer(AppConfig.GetConnectionString()));

        builder.AddScoped<IRepository<Category>, CategoryRepository>();
        builder.AddScoped<IRepository<Product>, ProductRepository>();

        builder.AddScoped<CategoryService>();
        builder.AddScoped<ProductService>();

        builder.AddTransient<MainViewModel>();
        builder.AddTransient<ProductViewModel>();
        builder.AddTransient<CategoryViewModel>();
        builder.AddTransient<ProductsFromCategotyViewModel>();

        builder.AddSingleton<Animation>();
        builder.AddSingleton<Navigation>();

        _provider = builder.BuildServiceProvider();
    }

    public static MainViewModel MainViewModel => _provider.GetRequiredService<MainViewModel>();
    public static ProductViewModel ProductViewModel => _provider.GetRequiredService<ProductViewModel>();
    public static CategoryViewModel CategoryViewModel => _provider.GetRequiredService<CategoryViewModel>();
    public static ProductsFromCategotyViewModel ProductsFromCategotyViewModel => _provider.GetRequiredService<ProductsFromCategotyViewModel>();
}
