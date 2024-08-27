using System.Security.Claims;
using FluentAssertions;
using Moq;

namespace Tests;

public class ReferralServiceTests
{
    [Fact]
    public async void GetDeepLink_ReturnsOK()
    {
        //Arrange
        var repository = new Mock<IReferralRepository>();
        repository.Setup(repo => repo.GetReferralCode("testuser")).ReturnsAsync("ABCDE");
        var deepLinkService = new Mock<IDeepLinkService>();
        deepLinkService.Setup(svc => svc.GetBaseUrl()).Returns("http://test.link/123");
        var sut = new ReferralService(repository.Object, deepLinkService.Object);

        var claimsPrincipal = new Mock<ClaimsPrincipal>();
        claimsPrincipal.Setup(cp => cp.FindFirst(ClaimTypes.Name))
            .Returns(new Claim(ClaimTypes.Name, "testuser"));

        //Act
        var result = await sut.GetDeepLink(claimsPrincipal.Object);

        //Assert
        result.Should().NotBeNull();
        result.BaseUrl.Should().Be("http://test.link/123");
        result.ReferralCode.Should().Be("ABCDE");
        result.Link.Should().Be("http://test.link/123?referral_code=ABCDE");
    }

    [Fact]
    public async Task GetDeepLink_WithInvalidUserName_ThrowsExceptionAsync()
    {
        //Arrange
        var repository = new Mock<IReferralRepository>();
        var deepLinkService = new Mock<IDeepLinkService>();
        var sut = new ReferralService(repository.Object, deepLinkService.Object);

        //Act
        var claimsPrincipal = new Mock<ClaimsPrincipal>();
        claimsPrincipal.Setup(cp => cp.FindFirst(ClaimTypes.Name))
            .Returns((Claim)null!);

        //Assert
        await Assert.ThrowsAsync<UserNameNotFoundException>(() => sut.GetDeepLink(claimsPrincipal.Object));
    }
}