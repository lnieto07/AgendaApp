using SQLite;

namespace AgendaApp.Models
{
    public abstract class BaseModels
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
