using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
