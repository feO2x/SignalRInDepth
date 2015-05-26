using FluentAssertions;
using Microsoft.AspNet.SignalR.Client;
using Xunit;

namespace SignalRAutomatedTests
{
    public sealed class ClientConnectionTests
    {
        [Fact]
        public void HubConnectionReturnsSameInstancesOnSubsequentCreateHubProxyCalls()
        {
            var hubConnection = new HubConnection("Foo");

            var firstProxy = hubConnection.CreateHubProxy("Baz");
            var secondProxy = hubConnection.CreateHubProxy("Baz");

            firstProxy.Should().BeSameAs(secondProxy);
        }
    }
}
