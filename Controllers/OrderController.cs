using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.DTO;
using WebApplication1.Repository;
using WebApplication1.Services.Interface;

namespace WebApplication1.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;
    private readonly ILogger<OrderController> _logger;

    public OrderController(OrderService orderService, ILogger<OrderController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<OrderDTO>>> GetAll()
    {
        List<OrderDTO> ordersDTO = await _orderService.GetAll();

        return Ok(ordersDTO);
    }
    
    [HttpGet]
    public async Task<ActionResult<OrderDTO>> GetById(int id)
    {
        OrderDTO order = await _orderService.GetById(id);
        
        return Ok(order);
    }
    
    [HttpPatch]
    public async Task<ActionResult<OrderDTO>> Patch(OrderPatchDTO orderPatch)
    {
        Console.WriteLine("patch");
        OrderDTO modifiedOrder = await _orderService.Update(orderPatch);
        Console.WriteLine("patchOK");
        return Ok(modifiedOrder);
    }
    
    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        await _orderService.Delete(id);
        
        return Ok();
    }
}