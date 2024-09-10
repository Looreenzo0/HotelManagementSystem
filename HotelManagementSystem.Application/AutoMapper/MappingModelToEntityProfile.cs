
using AutoMapper;
using HotelManagementSystem.Application.Models;
using HotelManagementSystem.Domain.Entities;

namespace HotelManagementSystem.Application.AutoMapper
{
    public class MappingModelToEntityProfile : Profile
    {
        public MappingModelToEntityProfile()
        {
            CreateMap<ReservationModel, Reservation>();
            CreateMap<Reservation, ReservationModel>();

            CreateMap<HotelRoomModel, HotelRoom>();
            CreateMap<HotelRoom, HotelRoomModel>();
        }
    }
}
