using AutoMapper;
using GreenLineSystems.Core.Interfaces;
using GreenLineSystems.Core.ViewModels;
using GreenLineSystems.Data.Context;
using GreenLineSystems.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GreenLineSystems.Core.Services;

public class AirportService:IAirportService
{
    private readonly GreenLineContext _context;
    private readonly ILogger<AirportService> _logger;
    private readonly IMapper _mapper;
    public AirportService(GreenLineContext context, ILogger<AirportService> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }


    public async Task<MessageResult<bool>> UploadAirports(List<AirportsUpload> model)
    {
        
        MessageResult<bool> serviceResponse = new MessageResult<bool>();
        try
        {
            using (_context)
            {
                var airports = _mapper.Map<List<Airports>>(model);
                await _context.BulkInsertAsync(airports);
                await _context.SaveChangesAsync();

                serviceResponse.Data = true;
                serviceResponse.Code = 200;
                serviceResponse.Message = "File Uploaded Successfully";
            }
        }
        catch (Exception e)
        {
            _logger.LogError("exception Adding Users:"+ e.StackTrace);

            serviceResponse.Code = 500;
            serviceResponse.Message = "error adding user: " + e.Message;
        }

        return serviceResponse;
    }
    

    public async Task<MessageResult<List<GetAirportsViewModel>>> GetAirports()
    {
        MessageResult<List<GetAirportsViewModel>> serviceResponse = new MessageResult<List<GetAirportsViewModel>>();
        

        try
        {
            using (_context)
            {
                var airports = await (from xc in _context.Airports
                    select new GetAirportsViewModel()
                    {
                        Id = xc.Id,
                        IATACode = xc.IATA,
                        ISOAlphaCode = xc.ISOAlpha,
                        LongName = xc.LongName,
                        LongLocation = xc.LongLocation
                        
                    }).ToListAsync();

                serviceResponse.Data = airports;
                serviceResponse.Code = 200;
                serviceResponse.Message = "Airports Fetched Successfully";
                
            }
        }
        catch (Exception e)
        {
            _logger.LogError("exception Adding Users:"+ e.StackTrace);

            serviceResponse.Code = 500;
            serviceResponse.Message = "error adding user: " + e.Message;
        }

        return serviceResponse;
    }
}