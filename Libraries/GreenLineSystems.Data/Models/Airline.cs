using System.ComponentModel.DataAnnotations.Schema;

namespace GreenLineSystems.Data.Models;

public class Airline
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string LetterCode { get; set; }
    public string Country { get; set; }
}