
using Microsoft.Extensions.Logging;
using ProyectoCrudF.DataAccess;
using ProyectoCrudF.ViewModels;
using ProyectoCrudF.Views;

namespace ProyectoCrudF;

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

        var dbContext = new UsuarioDbContext();
        dbContext.Database.EnsureCreated();
        dbContext.Dispose();

        builder.Services.AddDbContext<UsuarioDbContext>();

        builder.Services.AddTransient<UsuarioPage>();
        builder.Services.AddTransient<UsuarioViewModel>();

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<MainViewModel>();

        Routing.RegisterRoute(nameof(UsuarioPage), typeof(UsuarioPage));



#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
