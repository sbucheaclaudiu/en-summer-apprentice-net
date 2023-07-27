using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Targets;
using WebApplication1.Models;
using WebApplication1.Models.DTO;
using WebApplication1.Repository;

namespace WebApplication1.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class EventController : ControllerBase
{
    private readonly IEventRepo _eventRepo;
    private readonly IMapper _mapper;
    private readonly ILogger<EventController> _logger;

    public EventController(IEventRepo eventRepo, IMapper mapper, ILogger<EventController> logger)
    {
        _eventRepo = eventRepo;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<EventDTO>>> GetAll()
    {
        var events =  await _eventRepo.GetAll();
        
        var eventDTO = _mapper.Map<List<EventDTO>>(events);
        
        return Ok(eventDTO);
    }

    [HttpGet]
    public async Task<ActionResult<EventDTO>> GetById(int id)
    {

        var @event = await _eventRepo.GetByID(id);
        
        return Ok(_mapper.Map<EventDTO>(@event));
    }

    [HttpPatch]
    public async Task<ActionResult<EventDTO>> Patch(EventPatchDTO eventPatch)
    {
        var eventEntity = await _eventRepo.GetByID(eventPatch.EventID);
        
        if (@eventEntity == null)
        {
            return NotFound();
        }

        if (!eventPatch.EventName.IsNullOrEmpty())
            eventEntity.EventName = eventPatch.EventName;
        if (!eventPatch.Description.IsNullOrEmpty())
            eventEntity.Description = eventPatch.Description;

        _eventRepo.Update(eventEntity);
        return Ok(_mapper.Map<EventDTO>(eventEntity));
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        var eventEntity = await _eventRepo.GetByID(id);
        
        if (eventEntity == null)
        {
            return NotFound();
        }
        
        _eventRepo.Delete(eventEntity);
        return Ok();
    }
}