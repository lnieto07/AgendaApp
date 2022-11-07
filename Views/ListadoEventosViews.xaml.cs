namespace AgendaApp.Views;

public partial class ListadoEventosViews : ContentPage
{
	public ListadoEventosViews()
	{
		BindingContext = App.Current.Services.GetService<ListadoEventosViewModels>();
		InitializeComponent();	
	}

}