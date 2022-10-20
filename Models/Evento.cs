using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.Models
{
    public class Evento
    {
        [PrimaryKey, AutoIncrement]
        public int IdEvento { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaAlta { get; set; }
        public DateTime FechaEvento { get; set; }
        [Indexed]
        public int IdEstado { get; set; }
        [Indexed]
        public int IdTipoEvento { get; set; }


    }
}
