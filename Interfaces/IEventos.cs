

namespace AgendaApp.Interfaces;

public interface IEventos
{
    public Task<List<Evento>> GetAll();
    public Task<Evento> GetById(int id);
    public Task<int> InsertEvento(Evento evento);
    public Task<int> DeleteEvento(Evento evento);
    public Task<int> UpdateEvento(Evento evento);
}
