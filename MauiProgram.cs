using EasyShop.Data;
using EasyShop.Services;
using EasyShop.ViewModels;
using EasyShop.Views;
using Microsoft.Extensions.Logging;

namespace EasyShop;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		// Camada de Dados
		builder.Services.AddSingleton<CustomerRepository>();
		builder.Services.AddSingleton<ProductRepository>();

		// Camada de Negócio
		builder.Services.AddSingleton<CustomerService>();
		builder.Services.AddSingleton<ProductService>();

		// Camada de Apresentação
		builder.Services.AddTransient<CustomersViewModel>();
		builder.Services.AddTransient<ProductsViewModel>();
		builder.Services.AddTransient<CustomersPage>();
		builder.Services.AddTransient<ProductsPage>();

		return builder.Build();
	}
}