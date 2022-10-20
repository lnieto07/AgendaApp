using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.Models
{
    public class Estado
    {
        [PrimaryKey, AutoIncrement]
        public int IdEstado { get; set; }

        [SQLite.MaxLength(10), Unique]
        public string Descripcion { get; set; }

    }
}
