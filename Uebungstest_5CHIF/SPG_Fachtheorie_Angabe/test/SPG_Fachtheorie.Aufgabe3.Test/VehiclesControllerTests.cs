using Xunit;

namespace Spg.Fachtheorie.Aufgabe3.API.Test
{
    [Collection("Sequential")]
    public class VehiclesControllerTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly TestWebApplicationFactory _factory;

        public VehiclesControllerTests(TestWebApplicationFactory factory)
        {
            _factory = factory;
        }

    }
}
