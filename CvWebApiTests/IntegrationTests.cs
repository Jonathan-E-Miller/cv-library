using Castle.Core.Internal;
using CvWebApi;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CvWebApiTests
{
  public class IntegrationTests
  {
    private CustomWebApplicationFactory<CvWebApi.Startup> _factory;
    private HttpClient _client;

    [OneTimeSetUp]
    public void CreateClientFromFactory()
    {
      _factory = new CustomWebApplicationFactory<Startup>();
      _client = _factory.CreateClient();
    }


    [TestCase("/weatherforecast")]
    public async Task TestEndPointReturnsContent(string endpoint)
    {
      // The endpoint or route of the controller action.
      var httpResponse = await _client.GetAsync(endpoint);

      var result = httpResponse.Content;

      string content = result.ReadAsStringAsync().Result;

      // make sure that the content was returned.
      Assert.IsFalse(content.IsNullOrEmpty());
    }
  }
}
