using AutoMapper;
using WebApplication1.Models.DTO;
using WebApplication1.Repository;
using WebApplication1.Services.Interface;

namespace WebApplication1.Services;

public class OrderServiceImpl : OrderService
{
    private readonly IOrderRepo _orderRepo;
    private readonly ITicketCategoryRepo _ticketCategoryRepo;
    private readonly IMapper _mapper;
    private readonly ILogger<OrderServiceImpl> _logger;

    public OrderServiceImpl(IOrderRepo eventRepo, ITicketCategoryRepo ticketCategoryRepo, IMapper mapper, ILogger<OrderServiceImpl> logger)
    {
        _orderRepo = eventRepo;
        _ticketCategoryRepo = ticketCategoryRepo;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<List<OrderDTO>> GetAll()
    {
        var orders = await _orderRepo.GetAll();
        
        var ordersDTO = _mapper.Map<List<OrderDTO>>(orders);
        return ordersDTO;
    }

    public async Task<OrderDTO> GetById(int id)
    {
        var order = await _orderRepo.GetByID(id);
        
        var orderDto = _mapper.Map<OrderDTO>(order);
        return orderDto;
    }

    public async Task<OrderDTO> Update(OrderPatchDTO orderPatch)
    {
        var order = await _orderRepo.GetByID(orderPatch.OrderId);
        
        var ticketCategory = await _ticketCategoryRepo.GetByDescriptionAndEventId(orderPatch.Description, order.TicketCategory.Event.EventId);
        
        order.TicketCategory = ticketCategory;
        order.NumberOfTickets = orderPatch.NumberOfTickets;
        order.TotalPrice = orderPatch.NumberOfTickets * ticketCategory.Price;

        await _orderRepo.Update(order);

        var orderDto = _mapper.Map<OrderDTO>(order);
        return orderDto;
    }

    public async Task Delete(int id)
    {
        var order = await _orderRepo.GetByID(id);
        
        await _orderRepo.Delete(order);
    }
}