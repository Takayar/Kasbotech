using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kasbotech_Identity.Contexts
{
    public class KasbonetIdentityDbContext:IdentityDbContext
    {
        public KasbonetIdentityDbContext(DbContextOptions<KasbonetIdentityDbContext> options):base(options)
        {
            
        }
    }
}
