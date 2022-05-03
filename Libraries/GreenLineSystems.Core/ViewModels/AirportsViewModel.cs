using Microsoft.AspNetCore.Http;

namespace GreenLineSystems.Core.ViewModels;

public class AirportsViewModel
{
    
}


public class GetAirportsViewModel
{
    public int Id { get; set; }
    public string IATACode { get; set; }
    public string ISOAlphaCode { get; set; }
    public string LongName  { get; set; }
    public string LongLocation { get; set; }
}

public class AirportsUpload
{
    public string IATACode { get; set; }
    public string ISOAlphaCode { get; set; }
    public string LongName  { get; set; }
    public string LongLocation { get; set; }
}

public class AirportsUploadViewModel
{
    public IFormFile airportFile { get; set; }
}