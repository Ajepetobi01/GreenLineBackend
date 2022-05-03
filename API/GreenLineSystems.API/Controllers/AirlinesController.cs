using System.Data;
using ExcelDataReader;
using GreenLineSystems.Core.Interfaces;
using GreenLineSystems.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace GreenLineSystems.API.Controllers;

public class AirlinesController : Controller
{
    private readonly IAirlineService _airlinesService;
    private readonly IHostingEnvironment _hostingEnvironment;
    public AirlinesController(IAirlineService airlineService, IHostingEnvironment hostingEnvironment)
    {
        _airlinesService = airlineService;
        _hostingEnvironment = hostingEnvironment;
    }

    [HttpPost("UploadAirline")]
   [Produces(typeof(MessageResult<bool>))]
   public async Task<IActionResult> UploadAirlines([FromForm] AirlinesUploadViewModel model)
   {
      
       List<AirlinesUpload> airlineList = new List<AirlinesUpload>();
       
                string FileName = string.Empty;
                string FilePath = string.Empty;
                string folderName = "AirlinesUpload";
                string webRoothPath = _hostingEnvironment.ContentRootPath;
                string newPath = Path.Combine(webRoothPath, folderName);

                if (!Directory.Exists(newPath))
                    Directory.CreateDirectory(newPath);
                if (model.airlineFile.Length > 0)
                {
                 
                    FileName = FileName.Insert(0, DateTime.Now.Millisecond.ToString());


                    FilePath = Path.Combine(newPath, FileName);

                  
                    using (var stream = new FileStream(FilePath, FileMode.Create))
                    {
                        model.airlineFile.CopyTo(stream);
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
                                    
                                    airlineList.Add(new AirlinesUpload()
                                    {
                                        Company = reader.GetValue(0).ToString(),
                                        LetterCode = reader.GetValue(1).ToString(),
                                        Country = reader.GetValue(2).ToString()
                                        
                                    });
                                    
                                    
                                }
                        }
                        catch (Exception ex)
                        {

                            throw new Exception("Excel file not in correct format. Please use the downloaded template for upload");
                        }

                      


                    }


                }
                
                airlineList.RemoveAt(0);
      
      
      var response = await _airlinesService.UploadFile(airlineList);
      return StatusCode(response.Code, response);
   }
   
   
   [HttpGet("GetAirlines")]
   [Produces(typeof(MessageResult<List<GetAirlinesViewModel>>))]
   public async Task<IActionResult> GetAirlines()
   {
       var response = await _airlinesService.GetAirlines();
       return StatusCode(response.Code, response);
   }
}