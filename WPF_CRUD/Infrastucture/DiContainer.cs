using BLL.Services;
using DAL.Abstractions;
using DAL.Context;
using DAL.Models;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

        _provider = builder.BuildServiceProvider();
    }
}
