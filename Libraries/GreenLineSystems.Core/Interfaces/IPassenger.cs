using GreenLineSystems.Core.ViewModels;
using GreenLineSystems.Data.Models;

namespace GreenLineSystems.Core.Interfaces;

public interface IPassenger
{
    Task<MessageResult<bool>> AddPassenger(NewPassengerDetails model);
    Task<MessageResult<bool>> AddBulkPassengers(List<PassengerDetails> model);
    Task<MessageResult<List<PassengerDetailsModel>>> GetPassengers();
    Task<MessageResult<PassengerDetailsModel>> GetPassengerById(int id);
    Task<MessageResult<PassengerDetailsModel>> GetPassengerByName(string name);
}