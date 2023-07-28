using Microsoft.EntityFrameworkCore;
using WebApplication1.Exceptions;
using WebApplication1.Models;
using WebApplication1.Models.DTO;
using WebApplication1.Services.Interface;

namespace WebApplication1.Repository;

public class EventRepo : IEventRepo
{
    private readonly TicketsSystemContext _dbContext;
        
    public EventRepo()
    {
        _dbContext = new TicketsSystemContext();
    }
    
    public async Task<IEnumerable<Event>> GetAll()
    {
        var events = await _dbContext.Events.Include(e => e.Venue).Include(e => e.EventType).ToListAsync();
        return events;
    }

    public async Task<Event> GetByID(int id)
    {
        var @event = await _dbContext.Events.Where(e => e.EventId == id).Include(e => e.Venue).Include(e => e.EventType).FirstOrDefaultAsync();

        if (@event == null)
        {
            throw new EntityNotFoundException(id, nameof(Event));
        }
        
        return @event;
    }

    public async Task Save(Event @event)
    {
        _dbContext.Add(@event);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Event @event)
    {
        _dbContext.Entry(@event).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Event @event)
    {
         _dbContext.Remove(@event);
         await _dbContext.SaveChangesAsync();
    }
}