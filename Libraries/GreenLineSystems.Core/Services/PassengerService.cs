using GreenLineSystems.Core.Interfaces;
using GreenLineSystems.Core.ViewModels;
using GreenLineSystems.Data.Context;
using GreenLineSystems.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GreenLineSystems.Core.Services;

public class PassengerService:IPassenger
{
    private readonly GreenLineContext _context;

    public PassengerService(GreenLineContext context)
    {
        _context = context;
    }

    public async Task<MessageResult<bool>> AddPassenger(NewPassengerDetails model)
    {
        
        try
        {
            using (_context)
            {
                _context.PassengerDetails.Add(new PassengerDetails()
                {
                    FirstName = model.firstName,
                    LastName = model.surname,
                    DateOfBirth = model.dob,
                    Address = model.address,
                    PassportNumber = model.PassportNumber,
                    Nationality = model.Nationality,
                    Narcotics = model.Narcotics,
                    Terrorism = model.Terrorism,
                    IllegalImmigration = model.IllegalImmigration,
                    Gender = model.Gender,
                    Revenue = model.Revenue
                    
                });

                await _context.SaveChangesAsync();

                return new MessageResult<bool>()
                {
                    Data = true, Message = "Added Successfully", Code = 200
                };
            }
        }
        catch (Exception e)
        {
            return new MessageResult<bool>()
            {
                Data = true, Message = "Error Occured "+e.StackTrace , Code = 500
            };
        }
        
        
    }

    public async Task<MessageResult<bool>> AddBulkPassengers(List<PassengerDetails> model)
    {
        try
        {
            using (_context)
            {
                //add bulk customers
                await _context.BulkInsertAsync(model);

                await _context.SaveChangesAsync();
                
                return new MessageResult<bool>()
                {
                    Data = true, Message = "Added Successfully", Code = 200
                };
            }
        }
        catch (Exception e)
        {
            return new MessageResult<bool>()
            {
                Data = true, Message = "Error Occured "+e.StackTrace , Code = 500
            };
        }
    }

    public async Task<MessageResult<List<PassengerDetailsModel>>> GetPassengers()
    {
        try
        {
            using (_context)
            {

                var passengers = await (from db in _context.PassengerDetails
                    select new PassengerDetailsModel()
                    {
                        firstName = db.FirstName,
                        lastName = db.LastName,
                        passengerDob = db.DateOfBirth,
                        passengerAddress = db.Address,
                        Passport = db.PassportNumber,
                        Nationality = db.Nationality,
                        Narcotics = db.Narcotics,
                        Terrorism = db.Terrorism,
                        IllegalImmigration = db.IllegalImmigration,
                        Gender = db.Gender,
                        Revenue = db.Revenue
                        

                    }).ToListAsync();

                return new MessageResult<List<PassengerDetailsModel>>()
                {
                    Data = passengers, Message = "Fetched Successfully", Code = 200
                };
            }
        }
        catch (Exception e)
        {
            return new MessageResult<List<PassengerDetailsModel>>()
            {
                Data = null, Message = "Error Occured "+e.StackTrace , Code = 500
            };
        }
    }

    public async Task<MessageResult<PassengerDetailsModel>> GetPassengerById(int id)
    {
        try
        {
            using (_context)
            {
                var passengers = await (from db in _context.PassengerDetails
                    where db.Id == id
                    select new PassengerDetailsModel()
                    {
                        firstName = db.FirstName,
                        lastName = db.LastName,
                        passengerDob = db.DateOfBirth,
                        passengerAddress = db.Address,
                        Passport = db.PassportNumber,
                        Nationality = db.Nationality,
                        Narcotics = db.Narcotics,
                        Terrorism = db.Terrorism,
                        IllegalImmigration = db.IllegalImmigration,
                        Gender = db.Gender,
                        Revenue = db.Revenue

                    }).FirstOrDefaultAsync();
                
                return new MessageResult<PassengerDetailsModel>()
                {
                    Data = passengers, Message = "Fetched Successfully", Code = 200
                };
            }

           
        }
        catch (Exception e)
        {
            return new MessageResult<PassengerDetailsModel>()
            {
                Data = null, Message = "Error Occured "+e.StackTrace , Code = 500
            };
        }
    }

    public async Task<MessageResult<PassengerDetailsModel>> GetPassengerByName(string name)
    {
        try
        {
            var passengers = await (from db in _context.PassengerDetails
                where db.LastName == name || db.FirstName == name
                select new PassengerDetailsModel()
                {
                    firstName = db.FirstName,
                    lastName = db.LastName,
                    passengerDob = db.DateOfBirth,
                    passengerAddress = db.Address,
                    Passport = db.PassportNumber,
                    Nationality = db.Nationality,
                    Narcotics = db.Narcotics,
                    Terrorism = db.Terrorism,
                    IllegalImmigration = db.IllegalImmigration,
                    Gender = db.Gender,
                    Revenue = db.Revenue

                }).FirstOrDefaultAsync();
            
            return new MessageResult<PassengerDetailsModel>()
            {
                Data = passengers, Message = "Fetched Successfully", Code = 200
            };
        }
        catch (Exception e)
        {
            return new MessageResult<PassengerDetailsModel>()
            {
                Data = null, Message = "Error Occured "+e.StackTrace , Code = 500
            };
        }
    }
}