

namespace AgendaApp.Services
{
    public class EventosRepo : IEventos
    {
        private readonly SQLLiteHelper<Evento> db;
        public EventosRepo()=> db = new();


        public Task<List<Evento>> GetAll()
            => Task.FromResult(db.GetAllData());

        public Task<Evento> GetById(int id) 
            => Task.FromResult(db.Get(id));

        
        public Task<int> InsertEvento(Evento e)
            => Task.FromResult(db.Add(e)); 
        

        public Task<int> DeleteEvento(Evento e) 
            => Task.FromResult(db.Delete(e));

        public Task<int> UpdateEvento(Evento e)
            => Task.FromResult(db.Update(e));
    }
}
