using System.Data;
using ExcelDataReader;
using GreenLineSystems.Core.Interfaces;
using GreenLineSystems.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace GreenLineSystems.API.Controllers;

public class FlightsController : Controller
{
    private readonly IFlightService _flightsService;
    private readonly IHostingEnvironment _hostingEnvironment;
    public FlightsController(IFlightService flightsService, IHostingEnvironment hostingEnvironment)
    {
        _flightsService = flightsService;
        _hostingEnvironment = hostingEnvironment;
    }
    
    [HttpGet("Flights")]
    [Produces(typeof(MessageResult<List<FlightVM>>))]
    public async Task<IActionResult> GetFlights()
    {
        var response = await _flightsService.GetFlightDetails();
        return StatusCode(response.Code, response);
    }
    
    [HttpGet("PassengerFlights")]
    [Produces(typeof(MessageResult<List<FlightPassengerDetails>>))]
    public async Task<IActionResult> GetPassengerFlights()
    {
        var response = await _flightsService.GetPassengersFlight();
        return StatusCode(response.Code, response);
    }
    
    [HttpPost("UploadFlight")]
   [Produces(typeof(MessageResult<bool>))]
   public async Task<IActionResult> UploadFlight([FromForm(Name="flightFile")] IFormFile airlineFile)
   {
      
       List<FlightsUpload> flightList = new List<FlightsUpload>();
       
                string FileName = string.Empty;
                string FilePath = string.Empty;
                string folderName = "FlgihtsUpload";
                string webRoothPath = _hostingEnvironment.ContentRootPath;
                string newPath = Path.Combine(webRoothPath, folderName);

                if (!Directory.Exists(newPath))
                    Directory.CreateDirectory(newPath);
                if (airlineFile.Length > 0)
                {
                 
                    FileName = FileName.Insert(0, DateTime.Now.Millisecond.ToString());


                    FilePath = Path.Combine(newPath, FileName);

                  
                    using (var stream = new FileStream(FilePath, FileMode.Create))
                    {
                        airlineFile.CopyTo(stream);
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
                                    
                                    //this will help with excel file having empty rows
                                    if(reader.GetValue(0) == null)
                                        continue;
                                    
                                    flightList.Add(new FlightsUpload()
                                    {
                                        FlightName = reader.GetValue(0).ToString(),
                                        PassengerSeat = reader.GetValue(1).ToString(),
                                        PassengerForeName = reader.GetValue(2).ToString(),
                                        PassengerSurname = reader.GetValue(3).ToString(),
                                        PassportNumber = reader.GetValue(4).ToString(),
                                        CountryOfIssue = reader.GetValue(5).ToString()
                                        
                                    });
                                    
                                    
                                }
                        }
                        catch (Exception ex)
                        {

                            throw new Exception("Excel file not in correct format. Please use the downloaded template for upload");
                        }

                      


                    }


                }
                
                flightList.RemoveAt(0);
      
      
      var response = await _flightsService.UploadFlights(flightList);
      return StatusCode(response.Code, response);
   }
   
    [HttpPost("UploadFlightDetails")]
   [Produces(typeof(MessageResult<bool>))]
   public async Task<IActionResult> UploadFlightDetails([FromForm(Name="flightDetailsFile")] IFormFile airlineFile)
   {
      
       List<FlightsDetailsUpload> flightDetailsList = new List<FlightsDetailsUpload>();
       
                string FileName = string.Empty;
                string FilePath = string.Empty;
                string folderName = "FlgihtsDetailsUpload";
                string webRoothPath = _hostingEnvironment.ContentRootPath;
                string newPath = Path.Combine(webRoothPath, folderName);

                if (!Directory.Exists(newPath))
                    Directory.CreateDirectory(newPath);
                if (airlineFile.Length > 0)
                {
                 
                    FileName = FileName.Insert(0, DateTime.Now.Millisecond.ToString());


                    FilePath = Path.Combine(newPath, FileName);

                  
                    using (var stream = new FileStream(FilePath, FileMode.Create))
                    {
                        airlineFile.CopyTo(stream);
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
                                    
                                    //this will help with excel file having empty rows
                                    if(reader.GetValue(0) == null)
                                        continue;

                                    int Capacity = 0;
                                    int Crew = 0;

                                    if (flightDetailsList.Count > 0)
                                    {
                                        Capacity = Convert.ToInt32(reader.GetValue(5));
                                        Crew = Convert.ToInt32(reader.GetValue(6));
                                    }

                                    flightDetailsList.Add(new FlightsDetailsUpload()
                                    {
                                        
                                        
                                        
                                        Flight = reader.GetValue(0).ToString(),
                                        FlightDeparture = reader.GetValue(1).ToString(),
                                        FlightArrival = reader.GetValue(2).ToString(),
                                        FlightTerminal = reader.GetValue(3).ToString(),
                                        Aircraft = reader.GetValue(4).ToString(),
                                        FlightCapacity = Capacity,
                                        FlgithCrew = Crew
                                        
                                    });
                                    
                                    
                                }
                        }
                        catch (Exception ex)
                        {

                            throw new Exception("Excel file not in correct format. Please use the downloaded template for upload");
                        }

                      


                    }


                }
                
                flightDetailsList.RemoveAt(0);
      
      
      var response = await _flightsService.UploadFlightDetails(flightDetailsList);
      return StatusCode(response.Code, response);
   }


   [HttpGet("FlightStat")]
   [Produces(typeof(MessageResult<FlightDashboard>))]
   public async Task<IActionResult> FlightStat()
   {
       var response = await _flightsService.GetFlightDashboard();
       return StatusCode(response.Code, response);
   }
}