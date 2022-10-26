using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.Models
{
    [Table("Evento")]
    public class Evento :BaseModels
    {

        //[PrimaryKey, AutoIncrement]
        //public int IdEvento { get; set; }
        [MaxLength(100)]
        public string Descripcion { get; set; } = "";
        public DateTime FechaFinEvento { get; set; } = DateTime.Now;
        public DateTime FechaEvento { get; set; } = DateTime.Now;
        [Indexed]
        public int IdEstado { get; set; }
        [Indexed]
        public int IdTipoEvento { get; set; }

    }
}
