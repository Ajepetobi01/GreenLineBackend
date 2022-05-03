using System.Data;
using ExcelDataReader;
using GreenLineSystems.Core.Interfaces;
using GreenLineSystems.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace GreenLineSystems.API.Controllers;

public class AirportsController : Controller
{
   private readonly IAirportService _airportsService;
   private readonly IHostingEnvironment _hostingEnvironment;
   public AirportsController(IAirportService airportsService, IHostingEnvironment hostingEnvironment)
   {
      _airportsService = airportsService;
      _hostingEnvironment = hostingEnvironment;
   }
   
   [HttpPost("UploadAirports")]
   [Produces(typeof(MessageResult<bool>))]
   public async Task<IActionResult> UploadAirports([FromForm] AirportsUploadViewModel model)
   {
      
       List<AirportsUpload> airpotList = new List<AirportsUpload>();
       
                string FileName = string.Empty;
                string FilePath = string.Empty;
                string folderName = "AirportsUpload";
                string webRoothPath = _hostingEnvironment.ContentRootPath;
                string newPath = Path.Combine(webRoothPath, folderName);

                if (!Directory.Exists(newPath))
                    Directory.CreateDirectory(newPath);
                if (model.airportFile.Length > 0)
                {
                 
                    FileName = FileName.Insert(0, DateTime.Now.Millisecond.ToString());


                    FilePath = Path.Combine(newPath, FileName);

                  
                    using (var stream = new FileStream(FilePath, FileMode.Create))
                    {
                        model.airportFile.CopyTo(stream);
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
                                    
                                    
                                    if(reader.GetValue(1) == null)
                                        continue;
                                    
                                    airpotList.Add(new AirportsUpload()
                                    {
                                        IATACode = reader.GetValue(0).ToString(),
                                        ISOAlphaCode = reader.GetValue(1).ToString(),
                                        LongName = reader.GetValue(2).ToString(),
                                        LongLocation = reader.GetValue(3).ToString()
                                        
                                    });
                                    
                                    
                                }
                        }
                        catch (Exception ex)
                        {

                            throw new Exception("Excel file not in correct format. Please use the downloaded template for upload");
                        }

                      


                    }


                }
                
                airpotList.RemoveAt(0);
      
      
      var response = await _airportsService.UploadAirports(airpotList);
      return StatusCode(response.Code, response);
   }
   
   
   [HttpGet("GetAirports")]
   [Produces(typeof(MessageResult<List<GetAirportsViewModel>>))]
   public async Task<IActionResult> GetAirports()
   {
       var response = await _airportsService.GetAirports();
       return StatusCode(response.Code, response);
   }
}