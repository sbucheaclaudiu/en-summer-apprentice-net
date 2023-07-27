using WebApplication1.Models;

namespace WebApplication1.Repository;

public interface ITicketCategoryRepo
{
    Task<TicketCategory> GetByDescriptionAndEventId(string description, int eventId);
}