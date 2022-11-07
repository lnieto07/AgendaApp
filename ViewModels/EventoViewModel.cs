using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.Maui.Platform;

namespace AgendaApp.ViewModels;
// [QueryProperty]()



//[QueryProperty(nameof(Id), "id")]
//[QueryProperty(nameof(Titulo), "titulo")]
//[QueryProperty(nameof(Descripcion), "descripcion")]
//[QueryProperty(nameof(FechaEvento), "fechaevento")]
//[QueryProperty(nameof(HoraEvento), "horaevento")]
//[QueryProperty(nameof(FechaFinEvento), "fechafinevento")]
//[QueryProperty(nameof(HoraFinEvento), "horafinevento")]



//[QueryProperty("Evento", "e")]
        [QueryProperty("Id", "Id")]
        [QueryProperty("Titulo", "Titulo")]
        [QueryProperty("Descripcion", "Descripcion")]
        [QueryProperty("FechaEvento", "FechaEvento")]
        [QueryProperty("HoraEvento", "HoraEvento")]
        [QueryProperty("FechaFinEvento", "FechaFinEvento")]
        [QueryProperty("HoraFinEvento", "HoraFinEvento")]
        
public partial class EventoViewModel : ObservableValidator 
    {
        private readonly IEventos _evento;

          public EventoViewModel()
      => this._evento = App.Current.Services.GetService<IEventos>();
   

    public ObservableCollection<string> Errores { get; set; } = new();


        [ObservableProperty]
        private string resultado;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsEnabled))]
        private bool isVisible;
        public bool IsEnabled => !IsVisible;
        [ObservableProperty]
        private int id;
       

    private string titulo;

    [Required(ErrorMessage = "El campo Título es obligatorio")]
    [SQLite.MaxLength(100)]
    public string Titulo
    {
        get => titulo;
        set => SetProperty(ref titulo, value, true);
    }

    private TimeSpan horaevento;
    [Required(ErrorMessage = "Es necesario colocar hora de Evento")]
    public TimeSpan HoraEvento
    {
        get => horaevento;
        set => SetProperty(ref horaevento, value, true);
    }

    private TimeSpan horafinevento;
    [Required(ErrorMessage = "Es necesario colocar hora de finalización")]
    public TimeSpan HoraFinEvento
    {
        get => horafinevento;
        set => SetProperty(ref horafinevento, value, true);
    }

    private string descripcion;

        [Required(ErrorMessage = "El campo Descripcion es obligatorio")]
        [SQLite.MaxLength(100)]
        public string Descripcion
        {
            get => descripcion;
            set => SetProperty(ref descripcion, value, true);
        }

        private DateTime fechaevento;

        [Required(ErrorMessage = "El campo Descripcion es obligatorio")]
        //[SQLite.MaxLength(100)]
        public DateTime FechaEvento
        {
            get => fechaevento;
            set => SetProperty(ref fechaevento, value, true);
        }
        private DateTime fechafinevento;

        [Required(ErrorMessage = "El campo Fin Evento es obligatorio")]
        //[SQLite.MaxLength(100)]
        public DateTime FechaFinEvento
        {
            get => fechafinevento;
            set => SetProperty(ref fechafinevento, value, true);
        }
        
    
        

    [RelayCommand]
        public async Task GuardarEvento(Evento E)
        {
            IsBusy = true;
            IsVisible = false;
            ValidateAllProperties();

            Errores.Clear();
            GetErrors(nameof(Descripcion)).ToList().ForEach(f => Errores.Add("Descripción: " + f.ErrorMessage));
            GetErrors(nameof(FechaEvento)).ToList().ForEach(f => Errores.Add("Fecha del Evento: " + f.ErrorMessage));
            GetErrors(nameof(FechaFinEvento)).ToList().ForEach(f => Errores.Add("Fecha de fin de Evento: " + f.ErrorMessage));
            GetErrors(nameof(Titulo)).ToList().ForEach(f => Errores.Add("Evento: " + f.ErrorMessage));

        IsBusy = false;
            if (Errores.Count > 0) return;



            IsBusy = true;
            if (Id == 0) Id = await _evento.InsertEvento(new Evento() { Descripcion = Descripcion, FechaEvento = FechaEvento, FechaFinEvento = FechaFinEvento, Titulo = Titulo, HoraEvento = HoraEvento, HoraFinEvento = HoraFinEvento });
            if (Id > 0) await _evento.UpdateEvento(new Evento() { Descripcion = Descripcion, FechaEvento = FechaEvento, FechaFinEvento = FechaFinEvento, Titulo = Titulo, HoraEvento = HoraEvento, HoraFinEvento = HoraFinEvento, Id = Id});
           


            Resultado = $" Registro id:{Id}";
            IsBusy = false;
            IsVisible = true;

            await Task.Delay(500);
            //await Shell.Current.Navigation.PopToRootAsync();
           // await Shell.Current.GoToAsync($"{nameof(ListadoEventosViews)}", false);
            await Shell.Current.Navigation.PushAsync(new ListadoEventosViews(), false);

    }

    //[RelayCommand]
    //public async Task AddNew()
    //{

        //    await Shell.Current.Navigation.PushAsync(new Evento(), false);
        //}
}
