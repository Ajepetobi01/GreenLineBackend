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

    }
}