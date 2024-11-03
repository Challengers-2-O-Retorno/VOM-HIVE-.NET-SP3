using VOM_HIVE.API.Data;

namespace VOM_HIVE.API.TESTS.Data
{
    public class TestDbContextFixture : IDisposable
    {
        public AppDbContext Context { get; private set; }

        public TestDbContextFixture()
        {
            Context = DBContextFactory.CreateInMemoryDbContext();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
