using AutoMapper;
using GreenLineSystems.Core.ViewModels;
using GreenLineSystems.Data.Models;

namespace GreenLineSystems.API.Mapping;

public class ClassMapping : Profile
{
    public ClassMapping()
    {
        CreateMap<AirportsUpload, Airports>()
            .ForMember(dest =>
                    dest.IATA,
                opt => opt.MapFrom(src => src.IATACode))
            .ForMember(dest =>
                    dest.ISOAlpha,
                opt => opt.MapFrom(src => src.ISOAlphaCode))
            .ForMember(dest =>
                dest.LongName, opt => opt.MapFrom(src => src.LongName))
            .ForMember(dest =>
                dest.LongLocation, opt => opt.MapFrom(src => src.LongLocation))
            .ReverseMap();
        
        CreateMap<AirlinesUpload, Airline>()
            .ForMember(dest =>
                    dest.CompanyName,
                opt => opt.MapFrom(src => src.Company))
            .ForMember(dest =>
                    dest.LetterCode,
                opt => opt.MapFrom(src => src.LetterCode))
            .ForMember(dest =>
                dest.Country, opt => opt.MapFrom(src => src.Country))
           .ReverseMap();
        
        
        CreateMap<FlightsUpload, Flights>()
            .ForMember(dest =>
                    dest.Seat,
                opt => opt.MapFrom(src => src.PassengerSeat))
            .ForMember(dest =>
                    dest.Surname,
                opt => opt.MapFrom(src => src.PassengerSurname))
            .ForMember(dest =>
                dest.ForeName, opt => opt.MapFrom(src => src.PassengerForeName))
            .ForMember(dest =>
                dest.FlightName, opt => opt.MapFrom(src => src.FlightName))
            .ForMember(dest =>
                dest.PassportNumber, opt => opt.MapFrom(src => src.PassportNumber))
            .ForMember(dest =>
                dest.CountryOfIssue, opt => opt.MapFrom(src => src.CountryOfIssue))
            
            .ReverseMap();
        
        
        CreateMap<FlightsDetailsUpload, FlightDetails>()
            .ForMember(dest =>
                    dest.Aircraft,
                opt => opt.MapFrom(src => src.Aircraft))
            .ForMember(dest =>
                    dest.Arrival,
                opt => opt.MapFrom(src => src.FlightArrival))
            .ForMember(dest =>
                dest.Capacity, opt => opt.MapFrom(src => src.FlightCapacity))
            .ForMember(dest =>
                dest.Crew, opt => opt.MapFrom(src => src.FlgithCrew))
            .ForMember(dest =>
                dest.Departure, opt => opt.MapFrom(src => src.FlightDeparture))
            .ForMember(dest =>
                dest.Flight, opt => opt.MapFrom(src => src.Flight))
            .ForMember(dest =>
                dest.Terminal, opt => opt.MapFrom(src => src.FlightTerminal))

            .ReverseMap();

    }
}