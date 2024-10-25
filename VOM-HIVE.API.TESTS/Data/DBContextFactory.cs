using Microsoft.EntityFrameworkCore;
using VOM_HIVE.API.Data;

namespace VOM_HIVE.API.TESTS.Data
{
    public class DBContextFactory
    {
        public static AppDbContext CreateInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                            .UseInMemoryDatabase(databaseName: "TestDB")
                            .Options;

            var context = new AppDbContext(options);
            return context;
        }
    }
}
