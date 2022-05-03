using System.ComponentModel.DataAnnotations.Schema;

namespace GreenLineSystems.Data.Models;

public class Airports
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string IATA { get; set; }
    public string ISOAlpha { get; set; }
    public string LongName  { get; set; }
    public string LongLocation { get; set; }
}