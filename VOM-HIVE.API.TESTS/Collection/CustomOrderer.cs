using Xunit.Abstractions;
using Xunit.Sdk;

namespace VOM_HIVE.API.TESTS.Collection
{
    public class CustomOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            return testCases.OrderBy(tc => tc.TestMethod.Method
            .GetCustomAttributes(typeof(TestPriorityAttribute).AssemblyQualifiedName)
            .FirstOrDefault()?.GetNamedArgument<int>("Priority") ?? int.MaxValue);
        }
    }
}
