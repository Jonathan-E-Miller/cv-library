using Castle.Core.Internal;
using CvWebApi;
using CvWebApi.Utils;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CvWebApiTests
{
  [TestFixture]
  public class IntegrationTests
  {
    private CustomWebApplicationFactory<Startup> _factory;
    private HttpClient _client;

    [SetUp]
    public void CreateClientFromFactory()
    {
      _factory = new CustomWebApplicationFactory<Startup>();
      _client = _factory.CreateClient();
    }

    [TearDown]
    public void TearDown()
    {
      _client.Dispose();
      _factory.Dispose();
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

    [TestCase("test0@test.com", "pass12345", "User123", true, false)]       // bad password
    [TestCase("test1@test.com", "Pass12345!", "", true, false)]             // missing username
    [TestCase("test2@test.com", "Pass12345!", "User123", true, true)]      // should pass
    public async Task TestRegister(string email, string pass, string user, bool rememberMe, bool shouldPass)
    {
      ApiUser userObj = new ApiUser()
      {
        Email = email,
        Password = pass,
        Username = user
      };

      var json = JsonConvert.SerializeObject(userObj);

      StringContent strContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

      // The endpoint or route of the controller action.
      var httpResponse = await _client.PostAsync("auth/register", strContent);

      var result = httpResponse.Content;

      string content = result.ReadAsStringAsync().Result;

      CvApiResponse response =JsonConvert.DeserializeObject<CvApiResponse>(content);
      // make sure that the content was returned.
      Assert.AreEqual(shouldPass, response.Success);
    }
  }
}
