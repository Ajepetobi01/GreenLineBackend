using AutoMapper;
using GreenLineSystems.Core.Interfaces;
using GreenLineSystems.Core.ViewModels;
using GreenLineSystems.Data.Context;
using GreenLineSystems.Data.Models;
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
                        Arrival = xc.Arrival,
                        Flight = xc.Flight
                        
                        
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

    public async Task<MessageResult<bool>> UploadFlights(List<FlightsUpload> model)
    {
        MessageResult<bool> serviceResponse = new MessageResult<bool>();
        try
        {
            using (_context)
            {
                var airports = _mapper.Map<List<Flights>>(model);
                await _context.BulkInsertAsync(airports);
                await _context.SaveChangesAsync();

                serviceResponse.Data = true;
                serviceResponse.Code = 200;
                serviceResponse.Message = "File Uploaded Successfully";
            }
        }
        catch (Exception e)
        {
            _logger.LogError("exception Adding Flights:"+ e.StackTrace);

            serviceResponse.Code = 500;
            serviceResponse.Message = "error adding Flights: " + e.Message;
        }

        return serviceResponse;
    }

    public async Task<MessageResult<bool>> UploadFlightDetails(List<FlightsDetailsUpload> model)
    {
        MessageResult<bool> serviceResponse = new MessageResult<bool>();
        try
        {
            using (_context)
            {
                var airports = _mapper.Map<List<FlightDetails>>(model);
                await _context.BulkInsertAsync(airports);
                await _context.SaveChangesAsync();

                serviceResponse.Data = true;
                serviceResponse.Code = 200;
                serviceResponse.Message = "File Uploaded Successfully";
            }
        }
        catch (Exception e)
        {
            _logger.LogError("exception Adding FlightsDetails:"+ e.StackTrace);

            serviceResponse.Code = 500;
            serviceResponse.Message = "error adding FlightsDetails: " + e.Message;
        }

        return serviceResponse;
    }

    public async Task<MessageResult<FlightDashboard>> GetFlightDashboard()
    {
        MessageResult<FlightDashboard> response = new MessageResult<FlightDashboard>();
        try
        {
            using (_context)
            {
                FlightDashboard dashboard = new FlightDashboard();
                List<FlightReportList> flightreport = new List<FlightReportList>();
                List<string> categories = new List<string>();

                categories.Add("Smuggling"); 
                categories.Add("Terrorism"); 
                categories.Add("Narcotics");
                categories.Add("IllegalImmigration");
                categories.Add("Revenue");
                
                dashboard.Categories = categories;
                
                //get all flights data
                var flights = await _context.Flights.ToListAsync();

                foreach (var flight in flights)
                {
                    FlightReportList reportList = new FlightReportList();

                    reportList.FlightName = flight.FlightName;
                        

                    var passengerDetails = await _context.PassengerDetails.Where(x => x.FirstName
                        == flight.ForeName && x.LastName == flight.Surname).FirstOrDefaultAsync();

                    if (passengerDetails != null)
                    {
                        List<double> datapoints = new List<double>();
                        
                        datapoints.Add(passengerDetails.Smuggling);
                        datapoints.Add(passengerDetails.Terrorism);
                        datapoints.Add(passengerDetails.Narcotics);
                        datapoints.Add(passengerDetails.IllegalImmigration);
                        datapoints.Add(passengerDetails.Revenue);

                        reportList.DatePoints = datapoints;
                    }
                    else
                    {
                        List<double> datapointss = new List<double>();
                        
                        datapointss.Add(0);
                        datapointss.Add(0);
                        datapointss.Add(0);
                        datapointss.Add(0);
                        datapointss.Add(0);

                        reportList.DatePoints = datapointss;
                    }

                    flightreport.Add(reportList);

                }

                dashboard.ReportList = flightreport;

                response.Data = dashboard;
                response.Code = 200;
                response.Message = "Report successful";

            }
        }
        catch (Exception e)
        {
            _logger.LogError("Exception getting flight Dashboard:"+ e.StackTrace);

            response.Code = 500;
            response.Message = "error getting flight Dashboard: " + e.Message;
        }

        return response;
    }
}