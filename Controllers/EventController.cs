using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Targets;
using WebApplication1.Models;
using WebApplication1.Models.DTO;
using WebApplication1.Repository;
using WebApplication1.Services.Interface;

namespace WebApplication1.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly EventService _eventService;
    private readonly ILogger<EventController> _logger;

    public EventController(EventService eventService, ILogger<EventController> logger)
    {
        _eventService = eventService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<EventDTO>>> GetAll()
    {
        var events =  await _eventService.GetAll();
        
        return Ok(events);
    }

    [HttpGet]
    public async Task<ActionResult<EventDTO>> GetById(int id)
    {

        var @event = await _eventService.GetById(id);
        
        return Ok(@event);
    }

    [HttpPatch]
    public async Task<ActionResult<EventDTO>> Patch(EventPatchDTO eventPatch)
    {
        var modifiedEvent = _eventService.Update(eventPatch);
        
        return Ok(modifiedEvent);
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        _eventService.Delete(id);
        
        return Ok();
    }
}