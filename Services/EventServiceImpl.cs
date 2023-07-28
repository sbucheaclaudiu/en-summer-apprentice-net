using System.ComponentModel.Design;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Models;
using WebApplication1.Models.DTO;
using WebApplication1.Repository;
using WebApplication1.Services.Interface;

namespace WebApplication1.Services;

public class EventServiceImpl : EventService
{
    private readonly IEventRepo _eventRepo;
    private readonly IMapper _mapper;
    private readonly ILogger<EventServiceImpl> _logger;

    public EventServiceImpl(IEventRepo eventRepo, IMapper mapper, ILogger<EventServiceImpl> logger)
    {
        _eventRepo = eventRepo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<EventDTO>> GetAll()
    {
        var events =  await _eventRepo.GetAll();
        
        var eventsDTO = _mapper.Map<List<EventDTO>>(events);
        return eventsDTO;
    }

    public async Task<EventDTO> GetById(int id)
    {
        var eventEntity = await _eventRepo.GetByID(id);
        
        return _mapper.Map<EventDTO>(eventEntity);
    }

    public async Task<EventDTO> Update(EventPatchDTO eventPatch)
    {
        var eventEntity = await _eventRepo.GetByID(eventPatch.EventID);

        if (!eventPatch.EventName.IsNullOrEmpty())
            eventEntity.EventName = eventPatch.EventName;
        if (!eventPatch.Description.IsNullOrEmpty())
            eventEntity.Description = eventPatch.Description;

        await _eventRepo.Update(eventEntity);

        var eventDTO = _mapper.Map<EventDTO>(eventEntity);

        return eventDTO;
    }

    public async Task Delete(int id)
    {
        var eventEntity = await _eventRepo.GetByID(id);
        
        await _eventRepo.Delete(eventEntity);
    }
}