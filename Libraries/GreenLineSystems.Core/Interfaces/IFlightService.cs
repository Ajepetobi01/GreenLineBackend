using GreenLineSystems.Core.ViewModels;

namespace GreenLineSystems.Core.Interfaces;

public interface IFlightService
{
    Task<MessageResult<List<FlightVM>>> GetPassengersFlight();

    Task<MessageResult<List<FlightPassengerDetails>>> GetFlightDetails();

    Task<MessageResult<bool>> UploadFlights(List<FlightsUpload> model);

    Task<MessageResult<bool>> UploadFlightDetails(List<FlightsDetailsUpload> model);
}