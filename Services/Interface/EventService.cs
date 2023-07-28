using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTO;

namespace WebApplication1.Services.Interface;

public interface EventService
{
    public Task<List<EventDTO>> GetAll();

    public Task<EventDTO> GetById(int id);

    public Task<EventDTO> Update(EventPatchDTO eventPatch);

    public Task Delete(int id);
}