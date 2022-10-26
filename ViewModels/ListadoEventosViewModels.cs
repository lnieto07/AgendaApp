
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AgendaApp.ViewModels;

public partial class ListadoEventosViewModels : ObservableObject
{
    //public ObservableCollection<Evento> Eventos { get; set; } = new();

    //[RelayCommand]
    //public async Task ListarEventos()
    //{
    //    //Eventos.Clear();
    //    //Eventos.Add(new Evento() { Descripcion = "Reunion de trabajo Net Maui", FechaEvento = "24/10/2022", IdEstado = 1, IdTipoEvento = 1 });
    //    //Eventos.Add(new Evento() { Descripcion = "Reunion de Pil ", FechaEvento = "25/10/2022", IdEstado = 1, IdTipoEvento = 1 });
    //    //Eventos.Add(new Evento() { Descripcion = "Cumpleaños de pedro", FechaEvento = "28/10/2022", IdEstado = 1, IdTipoEvento = 2 });
    //    //Eventos.Add(new Evento() { Descripcion = "Reunion de trabajo Net Maui", FechaEvento = "30/10/2022", IdEstado = 1, IdTipoEvento = 1 });
    //}

    private readonly IEventos _eventosservicio;
    private readonly IDialogService _dialog;
    public ObservableCollection<Evento> Eventos { get; set; } = new();

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
        Task.Run(async () => await ListarEventos());

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
    public async Task EditarAlumno(Evento e)
    {

        await Shell.Current.GoToAsync($"/Evento?Id={e.Id}&Descripción={e.Descripcion}&Fecha de Evento={e.FechaEvento}", false);

    }

    [RelayCommand]
    public async Task EliminarAlumno(Evento e)
    {

        IsLoading = true;
        var res = await _dialog.ShowAlertAsync("Eliminar", $"Desea eliminiar el registro {e.Id}", "Aceptar", "Cancelar");
        if (!res) return;
        var A = await _eventosservicio.DeleteEvento(e);
        await ListarEventos();

    }

    [RelayCommand]
    public async Task AddNew()
    {

        await Shell.Current.Navigation.PushAsync(new EventoViews(), false);
    }

}