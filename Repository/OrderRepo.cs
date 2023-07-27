using Microsoft.EntityFrameworkCore;
using WebApplication1.Exceptions;
using WebApplication1.Models;

namespace WebApplication1.Repository;

public class OrderRepo : IOrderRepo
{
    private readonly TicketsSystemContext _dbContext;
        
    public OrderRepo()
    {
        _dbContext = new TicketsSystemContext();
    }
    
    public async Task<IEnumerable<Order>> GetAll()
    {
        return await _dbContext.Orders.Include(o => o.TicketCategory.Event).Include(o => o.Customer).Include(o=> o.TicketCategory).ToListAsync();
    }
    
    public async Task<Order> GetByID(int id)
    {
        var order = await _dbContext.Orders.Where(o => o.OrderId == id).Include(o => o.TicketCategory.Event).Include(o => o.Customer).Include(o=> o.TicketCategory).FirstOrDefaultAsync();
        
        if (order == null)
            throw new EntityNotFoundException(id, nameof(Event));
        
        return order;
    }

    public Task Save(Order order)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Order order)
    {
        _dbContext.Entry(order).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Order order)
    {
        _dbContext.Remove(order);
        await _dbContext.SaveChangesAsync();
    }
}