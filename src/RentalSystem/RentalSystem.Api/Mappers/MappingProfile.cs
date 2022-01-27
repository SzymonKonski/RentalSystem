using AutoMapper;
using RentalSystem.Api.Dto.Cars;
using RentalSystem.Api.Dto.Dealers;
using RentalSystem.Api.Dto.Reservations;
using RentalSystem.Domain.Entities;

namespace RentalSystem.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Car, CarListDto>();
            CreateMap<Car, CarDetailsDto>();
            CreateMap<CreateCarDto, Car>();
            CreateMap<UpdateCarDto, Car>();

            CreateMap<Domain.Entities.CarReservation, UserReservationDto>();

            CreateMap<Dealer, DealerDetailsDto>();
            CreateMap<Dealer, DealerListDto>();
            CreateMap<CreateDealerDto, Dealer>();
            CreateMap<UpdateDealerDto, Dealer>();
        }
    }
}
