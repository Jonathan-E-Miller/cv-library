using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using CvWebApi.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections;
using CvWebApi;
using System.Collections.Generic;

namespace CvWebApiTests
{
  public class UnitTests
  {
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestWeather()
    {
      Mock<ILogger<WeatherForecastController>> mockLogger = new Mock<ILogger<WeatherForecastController>>();
      WeatherForecastController wfc = new WeatherForecastController(mockLogger.Object);

      IEnumerable<WeatherForecast> forcasts = wfc.Get();

      Assert.IsNotNull(forcasts);
    }
  }
}