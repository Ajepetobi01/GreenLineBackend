using GreenLineSystems.Core.ViewModels;

namespace GreenLineSystems.Core.Interfaces;

public interface IAirlineService
{
    Task<MessageResult<List<GetAirlinesViewModel>>> GetAirlines();
    Task<MessageResult<bool>> UploadFile(List<AirlinesUpload> model);
}