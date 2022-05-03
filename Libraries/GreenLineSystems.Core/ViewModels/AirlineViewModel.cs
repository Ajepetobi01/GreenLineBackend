using Microsoft.AspNetCore.Http;

namespace GreenLineSystems.Core.ViewModels;

public class AirlineViewModel
{
    
}

public class GetAirlinesViewModel
{
    public int Id { get; set; }
    public string Company { get; set; }
    public string LetterCode { get; set; }
    public string Country  { get; set; }
}

public class AirlinesUpload
{
    public string Company { get; set; }
    public string LetterCode { get; set; }
    public string Country  { get; set; }
}

public class AirlinesUploadViewModel
{
    public IFormFile airlineFile { get; set; }
}