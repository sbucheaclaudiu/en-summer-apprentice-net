using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.DTO;
using WebApplication1.Repository;

namespace WebApplication1.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderRepo _orderRepo;
    private readonly IMapper _mapper;
    private readonly ITicketCategoryRepo _ticketCategoryRepo;

    public OrderController(IOrderRepo orderRepo, ITicketCategoryRepo ticketCategoryRepo, IMapper mapper)
    {
        _orderRepo = orderRepo;
        _ticketCategoryRepo = ticketCategoryRepo;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<OrderDTO>>> GetAll()
    {
        var orders = await _orderRepo.GetAll();
        
        var ordersDTO = _mapper.Map<List<OrderDTO>>(orders);
        
        return Ok(ordersDTO);
    }
    
    [HttpGet]
    public async Task<ActionResult<OrderDTO>> GetById(int id)
    {
        var order = await _orderRepo.GetByID(id);
        
        return Ok(_mapper.Map<OrderDTO>(order));
    }
    
    [HttpPatch]
    public async Task<ActionResult<OrderDTO>> Patch(OrderPatchDTO orderPatch)
    {
        var order = await _orderRepo.GetByID(orderPatch.OrderId);
        
        var ticketCategory = await _ticketCategoryRepo.GetByDescriptionAndEventId(orderPatch.Description, order.TicketCategory.Event.EventId);
        
        order.TicketCategory = ticketCategory;
        order.NumberOfTickets = orderPatch.NumberOfTickets;
        order.TotalPrice = orderPatch.NumberOfTickets * ticketCategory.Price;

        _orderRepo.Update(order);
        return Ok(_mapper.Map<OrderDTO>(order));
    }
    
    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        var order = await _orderRepo.GetByID(id);
        
        if (order == null)
        {
            return NotFound();
        }
        
        _orderRepo.Delete(order);
        return NoContent();
    }
}