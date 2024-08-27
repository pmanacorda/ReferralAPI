using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Tests;

public class DeepLinkServiceTests
{

    [Fact]
    public void GetBaseUrl_ReturnsOK()
    {
        //Arrange
        var configuration = new Mock<IConfiguration>();
        var expected = "TEST_URL";
        configuration.Setup(configuration => configuration["DEEP_LINK_URL"]).Returns(expected);
        var sut = new DeepLinkService(configuration.Object);

        //Act
        var result = sut.GetBaseUrl();

        //Assert
        result.Should().NotBeNull();
        result.Should().Be(expected);
    }

    [Fact]
    public void GetBaseUrl_WithMissingConfiguration_ThrowsException()
    {
        //Arrange
        var configuration = new Mock<IConfiguration>();

        //Act
        var sut = new DeepLinkService(configuration.Object);

        //Assert
        Assert.Throws<ConfigurationNotFoundException>(() => sut.GetBaseUrl());
    }
}