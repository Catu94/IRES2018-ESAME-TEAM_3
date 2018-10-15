using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gruppo3Esame.DataAccess
{
    public class SqlRepository : IdentityDbContext
    {
        public SqlRepository(DbContextOptions<SqlRepository> options)
            : base(options)
        { }
    }
}
