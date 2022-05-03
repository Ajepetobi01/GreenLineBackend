using GreenLineSystems.Core.ViewModels;
using GreenLineSystems.Data.Models;

namespace GreenLineSystems.Core.Interfaces;

public interface IAirportService
{
    Task<MessageResult<bool>> UploadAirports(List<AirportsUpload> model);

    Task<MessageResult<List<GetAirportsViewModel>>> GetAirports();
}