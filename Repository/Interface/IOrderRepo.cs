using WebApplication1.Models;

namespace WebApplication1.Repository;

public interface IOrderRepo
{
    Task<IEnumerable<Order>> GetAll();

    Task<Order> GetByID(int id);
    
    Task Save(Order order);

    Task Update(Order order);

    Task Delete(Order order);
}