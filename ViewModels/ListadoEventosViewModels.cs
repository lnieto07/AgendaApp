
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Globalization;
//using static Android.Provider.ContactsContract.CommonDataKinds;

namespace AgendaApp.ViewModels;

public partial class ListadoEventosViewModels : ObservableObject
{
        
    private readonly IEventos _eventosservicio;
    private readonly IDialogService _dialog;

    public ObservableCollection<Evento> Eventos { get; set; } = new();

    [ObservableProperty]
    private DateTime _currentDate =DateTime.Now;

    //prueba

    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private string titulo;
    [ObservableProperty]
    private string descripcion;
    [ObservableProperty]
    private DateTime fechaevento;
    [ObservableProperty]
    private DateTime fechafinevento;
    [ObservableProperty]
    private TimePicker horaevento;
    [ObservableProperty]
    private TimePicker horafinevento;


    //Prueba


    [ObservableProperty]
    Evento e;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsReady))]
    private bool isLoading;

    [ObservableProperty]
    bool isRefreshing;
    public bool IsReady => !IsLoading;


    public ListadoEventosViewModels()
    {
        _eventosservicio = App.Current.Services.GetService<IEventos>();
        _dialog = App.Current.Services.GetService<IDialogService>();
        Task.Run(async () => await ListarEventosDia());

    }


    [RelayCommand]
    public async Task ListarEventos()
    {
        IsLoading = true;
        Eventos.Clear();
        var lista = await _eventosservicio.GetAll();
        foreach (var item in lista) Eventos.Add(item);
        IsLoading = false;
        IsRefreshing = false;
    }

    [RelayCommand]
    public async Task ListarEventosDia()
    {
  
        IsLoading = true;
        Eventos.Clear();
        var lista = await _eventosservicio.GetAll();
        //var lista = await _eventosservicio.GetByDay(_currentDate);
        var filtrarLista = lista.Where(evento => evento.FechaEvento.Date == CurrentDate.Date).ToList();
        foreach (var item in filtrarLista) Eventos.Add(item);
        IsLoading = false;
        IsRefreshing = false;
    }


    [RelayCommand]
    public static async Task EditarEvento(Evento e)
    {
        //await Shell.Current.Navigation.PushAsync(nameof(EventoViews),typeOf(EventoViewModel)));

        //await Shell.Current.GoToAsync($"{nameof(EventoViews)}", true,
        //    new Dictionary<string, object>
        //    {
        //        {"Evento",e}
        //    });
        //await Shell.Current.Navigation.PushAsync(new EventoViews(), false);
        //await Shell.Current.GoToAsync($"{nameof(EventoViews)}?Id={12}");
        //await Shell.Current.GoToAsync($"/EventoViews?Id={12}");
        try
        {
            //var horaPrueba = e.HoraEvento.ToString("t", CultureInfo.CreateSpecificCulture("en-us"));
            //var horaPrueba = new TimeSpan(8, 30, 00); &HoraEvento ={ e.HoraEvento.ToString("HH:mm tt")}
            await Shell.Current.GoToAsync($"{nameof(EventoViews)}?Id={e.Id}&Titulo={e.Titulo}&Descripcion={e.Descripcion}&FechaEvento={e.FechaEvento.ToString("d")}&FechaFinEvento={e.FechaFinEvento.ToString("d")}", false);
            //await Shell.Current.GoToAsync($"EventoViews?Id={e.Id}&Titulo={e.Titulo}&Descripcion={e.Descripcion}&FechaEvento={e.FechaEvento}&FechaFinEvento={e.FechaFinEvento}&HoraEvento={e.HoraEvento}&HoraFinEvento={e.HoraFinEvento}", false);
            //await Shell.Current.GoToAsync($"/EventoViews?Id={e.Id}&Titulo={e.Titulo}&Descripcion={e.Descripcion}&FechaEvento={e.FechaEvento.ToString("d")}&FechaFinEvento={e.FechaFinEvento.ToString("d")}", false);
        }
        catch (IndexOutOfRangeException ex)
        {

            throw new ArgumentOutOfRangeException(
            "Parameter index is out of range.", ex);
        }
       
        //await Shell.Current.GoToAsync($"/Alumno?Id={alumno.Id}&Nombre={alumno.Nombre}&Apellido={alumno.Apellido}", false);

    }

    [RelayCommand]
    public async Task EliminarEvento(Evento e)
    {

        IsLoading = true;
        //var res = await _dialog.ShowAlertAsync("Eliminar", $"Desea eliminiar el registro {e.Id}", "Aceptar", "Cancelar");
        //if (!res) return;
        var E = await _eventosservicio.DeleteEvento(e);
        await ListarEventosDia();

    }

    [RelayCommand]
    public async Task AddNew()
    {

        await Shell.Current.Navigation.PushAsync(new EventoViews(), false);
    }

}