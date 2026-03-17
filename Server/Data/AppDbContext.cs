using Microsoft.EntityFrameworkCore;

namespace Server.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
            
        }
    }
}
