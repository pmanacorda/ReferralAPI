using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests;

public class ReferralControllerTests
{
    [Fact]
    public async void GetReferralDeepLink_ReturnsOK()
    {
        // Arrange
        var referralService = new Mock<IReferralService>();
        var expectedDeepLink = new DeepLink
        {
            Link = "http://test.link/12345?referral_code=ABCDE",
            BaseUrl = "http://test.link/12345",
            ReferralCode = "ABCDE"
        };
        referralService.Setup(svc => svc.GetDeepLink(It.IsAny<ClaimsPrincipal>()))
            .ReturnsAsync(expectedDeepLink);
        var sut = new ReferralController(referralService.Object);

        // Act
        var result = await sut.GetReferralDeepLink();

        // Assert
        result.Should().NotBeNull();

        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();

        var response = okResult?.Value as ApiResponse<DeepLink>;
        response.Should().NotBeNull();
        response?.Content.Should().BeEquivalentTo(expectedDeepLink);
    }

    [Fact]
    public async Task GetReferralDeepLink_WithInvalidUserName_ReturnsBadRequestAsync()
    {
        // Arrange
        var referralService = new Mock<IReferralService>();
        var exception = new UserNameNotFoundException();
        referralService.Setup(svc => svc.GetDeepLink(It.IsAny<ClaimsPrincipal>()))
            .ThrowsAsync(exception);
        var expectedMessage = exception.Msg;
        var sut = new ReferralController(referralService.Object);

        // Act
        var result = await sut.GetReferralDeepLink();

        // Assert
        result.Should().NotBeNull();

        var badRequestResult = result.Result as BadRequestObjectResult;
        badRequestResult.Should().NotBeNull();

        var response = badRequestResult?.Value as ApiResponse<DeepLink>;
        response.Should().NotBeNull();
        response?.Messages.Count.Should().Be(1);
        response?.Messages[0].Should().Be(expectedMessage);
    }
}