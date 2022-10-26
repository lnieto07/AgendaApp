
using SQLite;
using System.Runtime.CompilerServices;

namespace AgendaApp.Helpers;

public class SQLLiteHelper<T> : SQLLiteBase where T: BaseModels, new()

{
    public List<T> GetAllData() 
        => _connection.Table<T>().ToList();

    public T Get(int id)
        => _connection.Table<T>().Where(w => w.Id == id).FirstOrDefault();

    public int Add(T row)
    {
        _connection.Insert(row);
        return row.Id;
    }

    public int Update(T row)
        => _connection.Update(row);

    public int Delete(T row)
        => _connection.Delete(row);

}

public class SQLLiteBase
{
    private string _rutaDB;
    public SQLiteConnection _connection;

    public SQLLiteBase()
    {
        _rutaDB = FileAccessHelper.GetPathFile("eventos.db3");
        if (_connection != null) return;
        _connection = new SQLiteConnection(_rutaDB);
        _connection.CreateTable<Evento>();

    }
}
