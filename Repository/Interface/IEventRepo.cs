using WebApplication1.Models;

namespace WebApplication1.Repository;

public interface IEventRepo
{
    Task<IEnumerable<Event>> GetAll();
    
    Task<Event> GetByID(int id);

    Task Save(Event @event);

    Task Update(Event @event);

    Task Delete(Event @event);
}