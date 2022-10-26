namespace AgendaApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(ListadoEventosViews), typeof(ListadoEventosViews));
		Routing.RegisterRoute(nameof(EventoViews), typeof(EventoViews));
	}
}
