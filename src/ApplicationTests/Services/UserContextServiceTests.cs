using System.Security.Claims;
using Application.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace ApplicationTests.Services;
public class UserContextServiceTests
{
    [Fact]
    public void GetUserId_WithAuthenticatedUser_ShouldReturnUserId()
    {
        //arrange
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, "1"),
            new(ClaimTypes.Name, "User"),
            new(ClaimTypes.Role, "User")
        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(claims));

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
        {
            User = user
        });

        var userContext = new UserContextService(httpContextAccessorMock.Object);

        //act
        var userId = userContext.GetUserId;

        //arrange
        userId.Should().Be(1);
    }
}