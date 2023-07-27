using AutoMapper;
using WebApplication1.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Profiles;

public class Profile : AutoMapper.Profile
{
    public Profile()
    {
        CreateMap<Event, EventDTO>()
            .ForMember(
                dest => dest.EventID,
                opt => opt.MapFrom(src => $"{src.EventId}"))
            .ForMember(
                dest => dest.EventName,
                opt => opt.MapFrom(src => $"{src.EventName}"))
            .ForMember(
                dest => dest.EventTypeName,
                opt => opt.MapFrom(src => $"{src.EventType.EventTypeName}"))
            .ForMember(
                dest => dest.Description,
                opt => opt.MapFrom(src => $"{src.Description}"))
            .ForMember(
                dest => dest.Location,
                opt => opt.MapFrom(src => $"{src.Venue.Location}"))
            .ReverseMap();
        
        CreateMap<Event, EventPatchDTO>()
            .ForMember(
                dest => dest.EventID,
                opt => opt.MapFrom(src => $"{src.EventId}"))
            .ForMember(
                dest => dest.EventName,
                opt => opt.MapFrom(src => $"{src.EventName}"))
            .ForMember(
                dest => dest.Description,
                opt => opt.MapFrom(src => $"{src.Description}"))
            .ForMember(
                dest => dest.StartDate,
                opt => opt.MapFrom(src => $"{src.StartDate}"))
            .ForMember(
                dest => dest.EndDate,
                opt => opt.MapFrom(src => $"{src.EndDate}"))
            .ReverseMap();
        
        CreateMap<Order, OrderDTO>()
            .ForMember(
                dest => dest.OrderId,
                opt => opt.MapFrom(src => $"{src.OrderId}"))
            .ForMember(
                dest => dest.EventName,
                opt => opt.MapFrom(src => $"{src.TicketCategory.Event.EventName}"))
            .ForMember(
                dest => dest.CustomerName,
                opt => opt.MapFrom(src => $"{src.Customer.CustomerName}"))
            .ForMember(
                dest => dest.TicketCategoryDescription,
                opt => opt.MapFrom(src => $"{src.TicketCategory.Description}"))
            .ForMember(
                dest => dest.NumberOfTickets,
                opt => opt.MapFrom(src => $"{src.NumberOfTickets}"))
            .ForMember(
                dest => dest.TotalPrice,
                opt => opt.MapFrom(src => $"{src.TotalPrice}"))
            .ReverseMap();
        
        CreateMap<Order, OrderPatchDTO>()
            .ForMember(
                dest => dest.OrderId,
                opt => opt.MapFrom(src => $"{src.OrderId}"))
            .ForMember(
                dest => dest.Description,
                opt => opt.MapFrom(src => $"{src.TicketCategory.Description}"))
            .ForMember(
                dest => dest.NumberOfTickets,
                opt => opt.MapFrom(src => $"{src.NumberOfTickets}"))
            .ReverseMap();
    }
}