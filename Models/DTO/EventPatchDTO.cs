namespace WebApplication1.Models.DTO;

public class EventPatchDTO
{
    public int EventID { get; set; }
    public string? EventName { get; set; }
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
}