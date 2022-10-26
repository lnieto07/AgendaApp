namespace AgendaApp.Views;

public partial class EventoViews : ContentPage
{
	public EventoViews()
	{
		BindingContext = App.Current.Services.GetService<EventoViewModel>();
		InitializeComponent();
	}
}