 using Microsoft.EntityFrameworkCore;
using Sp2.Models;

namespace VOM_HIVE.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ) : base( options )
        {
        }

        public DbSet<ProfileuserModel> Profile_user { get; set; }

        public DbSet<ProductModel> Product {  get; set; }

        public DbSet<CompanyModel> Company { get; set; }

        public DbSet<CampaignModel> Campaign { get; set; }
    }
}
