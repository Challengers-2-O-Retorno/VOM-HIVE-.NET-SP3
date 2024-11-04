using VOM_HIVE.API.TESTS.Data;

namespace VOM_HIVE.API.TESTS.Collection
{
    [CollectionDefinition("ApiTests")]
    [TestCaseOrderer("VOM_HIVE.API.TESTS.Collection.CustomOrderer", "VOM-HIVE.API.TESTS")]
    public class ApiTestCollection : ICollectionFixture<CustomWebApplicationFactory<Program>> { }
}
