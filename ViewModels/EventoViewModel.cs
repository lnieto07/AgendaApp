using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.Input;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace AgendaApp.ViewModels;
        
        [QueryProperty("Id", "Id")]
        [QueryProperty("Descripcion","Descripcion")]
        [QueryProperty("FechaEvento", "FechaEvento")]

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
        public async Task GuardarEvento(Evento A)
        {
            IsBusy = true;
            IsVisible = false;
            ValidateAllProperties();

            Errores.Clear();
            GetErrors(nameof(Descripcion)).ToList().ForEach(f => Errores.Add("Descripción: " + f.ErrorMessage));
            GetErrors(nameof(FechaEvento)).ToList().ForEach(f => Errores.Add("Fecha del Evento: " + f.ErrorMessage));
            GetErrors(nameof(FechaFinEvento)).ToList().ForEach(f => Errores.Add("Fecha de fin de Evento: " + f.ErrorMessage));
        IsBusy = false;
            if (Errores.Count > 0) return;


            IsBusy = true;
            if (Id == 0) Id = await _evento.InsertEvento(new Evento() { Descripcion = Descripcion, FechaEvento = FechaEvento, FechaFinEvento = FechaFinEvento });
            if (Id > 0) await _evento.UpdateEvento(new Evento() { Descripcion = Descripcion, FechaEvento = FechaEvento, FechaFinEvento= FechaFinEvento, Id= Id});

            Resultado = $" Registro id:{Id}";
            IsBusy = false;
            IsVisible = true;

            await Task.Delay(2000);
            await Shell.Current.Navigation.PopToRootAsync();
        }
    }
