using GreenLineSystems.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GreenLineSystems.Data.Context;

public class GreenLineContext:IdentityDbContext<ApplicationUser>
{
    public GreenLineContext(DbContextOptions options) : base(options)
    {
        
    }
    
    
    
    
}