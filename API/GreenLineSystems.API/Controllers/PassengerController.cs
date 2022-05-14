using System.Data;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using ExcelDataReader;
using GreenLineSystems.Core.Interfaces;
using GreenLineSystems.Core.ViewModels;
using GreenLineSystems.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenLineSystems.API.Controllers;

[ApiController]
[Route("[controller]")]
//[Authorize]
public class PassengerController : Controller
{
    private IPassenger _passenger;
    IHostEnvironment _hostingEnvironment;
    public PassengerController(IPassenger passenger, IHostEnvironment hostingEnvironment)
    {
        _passenger = passenger;
        _hostingEnvironment = hostingEnvironment;
    }
    
    // [HttpGet("")]
    // [Produces(typeof(MessageResult<PassengerDetailsModel>))]
    // public async Task<IActionResult> GetPassenger([FromQuery] int Id)
    // {
    //     var response = await _passenger.GetPassengerById(Id);
    //     return StatusCode(response.Code, response);
    // }
    //
    [HttpGet("GetPassengers")]
    [Produces(typeof(MessageResult<PassengerDetailsModel>))]
    public async Task<IActionResult> GetPassengers()
    {
        var response = await _passenger.GetPassengers();
        return StatusCode(response.Code, response);
    }
    
    // [HttpGet("name")]
    // [Produces(typeof(MessageResult<PassengerDetailsModel>))]
    // public async Task<IActionResult> GetPassenger([FromQuery] string name)
    // {
    //     var response = await _passenger.GetPassengerByName(name);
    //     return StatusCode(response.Code, response);
    // }
    //
    
    // [HttpPost("")]
    // [Produces(typeof(MessageResult<bool>))]
    // public async Task<IActionResult> PostPassenger([FromBody] NewPassengerDetails model)
    // {
    //     var response = await _passenger.AddPassenger(model);
    //     return StatusCode(response.Code, response);
    // }
    //
    [HttpPost("UploadPassenger")]
    [Produces(typeof(MessageResult<bool>))]
    public async Task<IActionResult> PostBulkPassenger([FromForm(Name="file")] IFormFile file)
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
                
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                
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
                                    //the terrorism value is in percentgate, we need to first get as string and then now 
                                    //remove the percentage sign and convert it to double

                                    var terror = ""; double newTerror = 0;
                                    var narco = "";double newNarco = 0;
                                    var smuggle = "";double newSmuggle = 0;
                                    var immi = "";double newImmi = 0;
                                    var revenue = ""; double newRevenue = 0;
                                    var dateTime = DateTime.Now;
                                    
                                    if (model.Count > 0)
                                    {
                                         terror = reader.GetValue(6).ToString(); terror = terror.Replace("%", "");  newTerror = Convert.ToDouble(reader.GetValue(6));
                                         narco = reader.GetValue(7).ToString(); narco = narco.Replace("%", "");  newNarco = Convert.ToDouble(reader.GetValue(7));
                                         smuggle = reader.GetValue(8).ToString(); smuggle = smuggle.Replace("%", "");  newSmuggle= Convert.ToDouble(smuggle);
                                         immi = reader.GetValue(9).ToString();immi = immi.Replace("%", "");  newImmi = Convert.ToDouble(immi);
                                         revenue = reader.GetValue(10).ToString();revenue = revenue.Replace("%", ""); newRevenue = Convert.ToDouble(revenue);
                                         dateTime = Convert.ToDateTime(reader.GetValue(3));
                                    }

                                   
                                    
                                    model.Add(new PassengerDetails()
                                    {
                                        FirstName = reader.GetValue(0).ToString(),
                                        LastName = reader.GetValue(1).ToString(),
                                        Gender = reader.GetValue(2).ToString(),
                                        
                                        DateOfBirth = dateTime,
                                        Nationality = reader.GetValue(4).ToString(),
                                        PassportNumber = reader.GetValue(5).ToString(),
                                        Terrorism = newTerror,
                                        Narcotics = newNarco,
                                        Smuggling = newSmuggle,
                                        IllegalImmigration = newImmi,
                                        Revenue = newRevenue,
                                        Address = ""
                                        
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