using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace AspNet7.Integration.Tests.Infrastructure
{
    public abstract class BaseControllerTest : IClassFixture<ApplicationFactoryTest<StartupTest>>
    {
        protected WebApplicationFactory<StartupTest> Factory { get; }

        public BaseControllerTest(ApplicationFactoryTest<StartupTest> factory)
        {
            Factory = factory;            
        }

        // Add you other helper methods here
    }
}