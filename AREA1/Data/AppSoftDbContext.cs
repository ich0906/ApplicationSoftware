using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AREA1.Data {
    public class AppSoftDbContext : DbContext {
        public AppSoftDbContext(DbContextOptions<AppSoftDbContext> options)
            : base(options) {
        }
    }
}