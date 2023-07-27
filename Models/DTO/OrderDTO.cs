namespace WebApplication1.Models.DTO;

public class OrderDTO
{
    public int OrderId { get; set; }
    
    public string? EventName { get; set; }
    
    public string? CustomerName { get; set; }

    public string? TicketCategoryDescription { get; set; }

    public int? NumberOfTickets { get; set; }

    public double? TotalPrice { get; set; }
}