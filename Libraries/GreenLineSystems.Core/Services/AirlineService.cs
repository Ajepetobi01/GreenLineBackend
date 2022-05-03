using AutoMapper;
using GreenLineSystems.Core.Interfaces;
using GreenLineSystems.Core.ViewModels;
using GreenLineSystems.Data.Context;
using GreenLineSystems.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GreenLineSystems.Core.Services;

public class AirlineService:IAirlineService
{
    
    private readonly GreenLineContext _context;
    private readonly ILogger<AirlineService> _logger;
    private readonly IMapper _mapper;
    public AirlineService(GreenLineContext context, ILogger<AirlineService> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }
    
    
    public async Task<MessageResult<List<GetAirlinesViewModel>>> GetAirlines()
    {
        MessageResult<List<GetAirlinesViewModel>> serviceResponse = new MessageResult<List<GetAirlinesViewModel>>();
        

        try
        {
            using (_context)
            {
                var airlines = await (from xc in _context.Airlines
                    select new GetAirlinesViewModel()
                    {
                        Id = xc.Id,
                        Company = xc.CompanyName,
                        LetterCode = xc.LetterCode,
                        Country = xc.Country
                        
                    }).ToListAsync();

                serviceResponse.Data = airlines;
                serviceResponse.Code = 200;
                serviceResponse.Message = "Airlines Fetched Successfully";
                
            }
        }
        catch (Exception e)
        {
            _logger.LogError("exception getting airlines"+ e.StackTrace);

            serviceResponse.Code = 500;
            serviceResponse.Message = "error getting airlines : " + e.Message;
        }

        return serviceResponse;
    }

    public async Task<MessageResult<bool>> UploadFile(List<AirlinesUpload> model)
    {
        MessageResult<bool> serviceResponse = new MessageResult<bool>();
        try
        {
            using (_context)
            {
                var airlines = _mapper.Map<List<Airline>>(model);
                await _context.BulkInsertAsync(airlines);
                await _context.SaveChangesAsync();

                serviceResponse.Data = true;
                serviceResponse.Code = 200;
                serviceResponse.Message = "File Uploaded Successfully";
            }
        }
        catch (Exception e)
        {
            _logger.LogError("exception Adding airlines:"+ e.StackTrace);

            serviceResponse.Code = 500;
            serviceResponse.Message = "error adding airlines: " + e.Message;
        }

        return serviceResponse;
    }
}