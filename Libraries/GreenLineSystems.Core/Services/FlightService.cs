using AutoMapper;
using GreenLineSystems.Core.Interfaces;
using GreenLineSystems.Core.ViewModels;
using GreenLineSystems.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GreenLineSystems.Core.Services;

public class FlightService:IFlightService
{
    private readonly GreenLineContext _context;
    private readonly ILogger<FlightService> _logger;
    private readonly IMapper _mapper;
    public FlightService(GreenLineContext context, ILogger<FlightService> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    
    
    public async Task<MessageResult<List<FlightVM>>> GetPassengersFlight()
    {
        MessageResult<List<FlightVM>> response = new MessageResult<List<FlightVM>>();
        try
        {
            using (_context)
            {
                var flights = await (from xc in _context.FlightDetails
                    select new FlightVM()
                    {
                        Id = xc.Id,
                        Aircraft = xc.Aircraft,
                        Departure = xc.Departure,
                        Terminal = xc.Terminal,
                        FlightCrew = xc.Crew,
                        FlightCapacity = xc.Capacity,
                        Arrival = xc.Arrival
                        
                        
                    }).ToListAsync();

                response.Data = flights;
                response.Code = 200;
                response.Message = "flights Fetched Successfully";

            }
        }
        catch (Exception e)
        {
            return new MessageResult<List<FlightVM>>()
            {
                Data = null, Message = "Error Occured "+e.StackTrace , Code = 500
            };
        }

        return response;
    }

    public async Task<MessageResult<List<FlightPassengerDetails>>> GetFlightDetails()
    {
        MessageResult<List<FlightPassengerDetails>> response = new MessageResult<List<FlightPassengerDetails>>();
        try
        {
            using (_context)
            {
                var flights = await (from xc in _context.Flights
                    select new FlightPassengerDetails()
                    {
                        Id = xc.Id,
                        CountryOfIssue = xc.CountryOfIssue,
                        FlightName = xc.FlightName,
                        PassengerForeName = xc.ForeName,
                        PassengerSurname = xc.Surname,
                        PassengerSeat = xc.Seat,
                        PassportNumber = xc.PassportNumber
                        
                        
                    }).ToListAsync();

                response.Data = flights;
                response.Code = 200;
                response.Message = "flights details Fetched Successfully";

            }
        }
        catch (Exception e)
        {
            return new MessageResult<List<FlightPassengerDetails>>()
            {
                Data = null, Message = "Error Occured "+e.StackTrace , Code = 500
            };
        }

        return response;
    }
}