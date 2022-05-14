using GreenLineSystems.Core.Interfaces;
using GreenLineSystems.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace GreenLineSystems.API.Controllers;

public class FlightsController : Controller
{
    private readonly IFlightService _flightsService;
    private readonly IHostingEnvironment _hostingEnvironment;
    public FlightsController(IFlightService flightsService, IHostingEnvironment hostingEnvironment)
    {
        _flightsService = flightsService;
        _hostingEnvironment = hostingEnvironment;
    }
    
    [HttpGet("Flights")]
    [Produces(typeof(MessageResult<List<FlightVM>>))]
    public async Task<IActionResult> GetFlights()
    {
        var response = await _flightsService.GetFlightDetails();
        return StatusCode(response.Code, response);
    }
    
    [HttpGet("PassengerFlights")]
    [Produces(typeof(MessageResult<List<FlightPassengerDetails>>))]
    public async Task<IActionResult> GetPassengerFlights()
    {
        var response = await _flightsService.GetPassengersFlight();
        return StatusCode(response.Code, response);
    }
}