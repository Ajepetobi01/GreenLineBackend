using System.Data;
using System.IO;
using System.Net;
using ExcelDataReader;
using GreenLineSystems.Core.Interfaces;
using GreenLineSystems.Core.ViewModels;
using GreenLineSystems.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenLineSystems.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PassengerController : Controller
{
    private IPassenger _passenger;
    IHostEnvironment _hostingEnvironment;
    public PassengerController(IPassenger passenger, IHostEnvironment hostingEnvironment)
    {
        _passenger = passenger;
        _hostingEnvironment = hostingEnvironment;
    }
    
    [HttpGet("")]
    [Produces(typeof(MessageResult<PassengerDetailsModel>))]
    public async Task<IActionResult> GetPassenger([FromQuery] int Id)
    {
        var response = await _passenger.GetPassengerById(Id);
        return StatusCode(response.Code, response);
    }
    
    [HttpGet("all")]
    [Produces(typeof(MessageResult<PassengerDetailsModel>))]
    public async Task<IActionResult> GetPassengers([FromQuery] int pageSize, int limit)
    {
        var response = await _passenger.GetPassengers(pageSize,limit);
        return StatusCode(response.Code, response);
    }
    
    [HttpGet("name")]
    [Produces(typeof(MessageResult<PassengerDetailsModel>))]
    public async Task<IActionResult> GetPassenger([FromQuery] string name)
    {
        var response = await _passenger.GetPassengerByName(name);
        return StatusCode(response.Code, response);
    }
    
    
    [HttpPost("")]
    [Produces(typeof(MessageResult<bool>))]
    public async Task<IActionResult> PostPassenger([FromBody] NewPassengerDetails model)
    {
        var response = await _passenger.AddPassenger(model);
        return StatusCode(response.Code, response);
    }
    
    [HttpPost("bulk")]
    [Produces(typeof(MessageResult<bool>))]
    public async Task<IActionResult> PostBulkPassenger([FromBody] IFormFile file)
    {

        List<PassengerDetails> model = new List<PassengerDetails>();
                string FileName = string.Empty;
                string FilePath = string.Empty;
                string folderName = "PassengerUpload";
                string webRoothPath = _hostingEnvironment.ContentRootPath;
                string newPath = Path.Combine(webRoothPath, folderName);

                if (!Directory.Exists(newPath))
                    Directory.CreateDirectory(newPath);
                if (file.Length > 0)
                {
                 
                    FileName = FileName.Insert(0, DateTime.Now.Millisecond.ToString());


                    FilePath = Path.Combine(newPath, FileName);

                  
                    using (var stream = new FileStream(FilePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }


                }
                else
                {
                    return StatusCode(500, "Invalid File");

                }
                
                
                using (var stream = System.IO.File.Open(FilePath, FileMode.Open, FileAccess.Read))
                {
                    using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                    {

                        DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                        {

                            ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                            {
                                UseHeaderRow = true
                            }
                        });

                        try
                        {
                            
                            
                                while (reader.Read()) //Each ROW
                                {
                                    
                                    model.Add(new PassengerDetails()
                                    {
                                        FirstName = reader.GetValue(0).ToString(),
                                        LastName = reader.GetValue(2).ToString(),
                                        DateOfBirth = Convert.ToDateTime(reader.GetValue(3)),
                                        Address = reader.GetValue(4).ToString()
                                        
                                    });
                                    
                                    
                                }
                        }
                        catch (Exception ex)
                        {

                            throw new Exception("Excel file not in correct format. Please use the downloaded template for upload");
                        }

                      


                    }


                }
                
                model.RemoveAt(0);
                
        var response = await _passenger.AddBulkPassengers(model);
        return StatusCode(response.Code, response);
    }
}