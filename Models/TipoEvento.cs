﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaApp.Models
{
    public class TipoEvento
    {
        [PrimaryKey, AutoIncrement]
        public int IdTipoEvento { get; set; }
        public string Descripcion { get; set; }
        public string NombreTipoEvento { get; set; }
    }
}
