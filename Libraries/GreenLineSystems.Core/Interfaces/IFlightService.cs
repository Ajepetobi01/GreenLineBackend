using GreenLineSystems.Core.ViewModels;

namespace GreenLineSystems.Core.Interfaces;

public interface IFlightService
{
    Task<MessageResult<List<FlightVM>>> GetPassengersFlight();

    Task<MessageResult<List<FlightPassengerDetails>>> GetFlightDetails();

}