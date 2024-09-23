using Xunit;

namespace DotNetBoilerplate.Tests.Integration.setup;

[CollectionDefinition(nameof(BoilerplateEndpointsTestsCollection))]
public class BoilerplateEndpointsTestsCollection : ICollectionFixture<BoilerplateEndpointsTestsFixture>
{
}