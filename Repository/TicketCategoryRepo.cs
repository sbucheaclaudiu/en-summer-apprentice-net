using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Exceptions;
using WebApplication1.Models;

namespace WebApplication1.Repository;

public class TicketCategoryRepo : ITicketCategoryRepo
{
    private readonly TicketsSystemContext _dbContext;
        
    public TicketCategoryRepo()
    {
        _dbContext = new TicketsSystemContext();
    }
    
    public async Task<TicketCategory> GetByDescriptionAndEventId(string description, int eventId)
    {
        var ticketCategory = await _dbContext.TicketCategories.Where(tc => tc.Description == description && tc.EventId == eventId).Include(tc => tc.Orders).FirstOrDefaultAsync();
        
        if (ticketCategory == null)
            throw new EntityNotFoundException(description, nameof(Event));
        
        return ticketCategory;
    }
}