using AgendaApp.Services;
namespace AgendaApp;

public partial class App : Application
{
	public new static App Current => (App)Application.Current;
	public IServiceProvider Services { get;}

	public App()
	{
		var services = new ServiceCollection();
		Services = ConfigureServices(services);

		InitializeComponent();

		MainPage = new AppShell();
	}

	 private static IServiceProvider ConfigureServices(ServiceCollection services)
	{
		//Services
		services.AddSingleton<IEventos, EventosRepo>();
        //services.AddSingleton<IEventos, EventosRepo>();


        //ViewModels
        services.AddTransient<ListadoEventosViewModels>();
		services.AddTransient<EventoViewModel>();

		//Views
		services.AddSingleton<ListadoEventosViews>();
		services.AddSingleton<EventoViews>();

		return services.BuildServiceProvider();
	}
}
