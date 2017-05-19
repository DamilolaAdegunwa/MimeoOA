using Microsoft.EntityFrameworkCore;

namespace Abp.EntityFrameworkCore
{
    public abstract class AbpDbContext : DbContext
    {
        public AbpDbContext(DbContextOptions options)
        {
        }
    }
}
