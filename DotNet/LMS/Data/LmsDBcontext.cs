namespace LMS.Models;
using Microsoft.EntityFrameworkCore;
 
public class LmsDBcontext : DbContext
{
    public LmsDBcontext(DbContextOptions<LmsDBcontext> options) : base(options)
    {
    }
 
    public DbSet<ComplaintModel> ComplaintTable { get; set; }
}
 