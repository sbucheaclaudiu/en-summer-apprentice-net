using WebApplication1.Models.DTO;

namespace WebApplication1.Services.Interface;

public interface OrderService
{
    public Task<List<OrderDTO>> GetAll();

    public Task<OrderDTO> GetById(int id);

    public Task<OrderDTO> Update(OrderPatchDTO orderPatch);

    public Task Delete(int id);
}