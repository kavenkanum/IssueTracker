using FluentAssertions;
using IssueTracker.Domain.Language;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace IssueTracker.UnitTests.ValueObjects
{
    public class CurrentUserTests
    {
        [Fact]
        public void ShouldHavePermission()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Bob"),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.NameIdentifier, "12345")
            };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var user = new CurrentUser(claimsPrincipal);

            user.HasPermission(Permission.DeleteJob).Should().BeTrue();
        }

        [Fact]
        public void ShouldNotHavePermission()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Bob"),
                new Claim(ClaimTypes.Role, "manager"),
                new Claim(ClaimTypes.NameIdentifier, "12345")
            };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var user = new CurrentUser(claimsPrincipal);
            
            user.HasPermission(Permission.AddJob).Should().BeFalse();
        }

        [Fact]
        public void UserIdShouldBeLong()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Bob"),
                new Claim(ClaimTypes.Role, "manager"),
                new Claim(ClaimTypes.NameIdentifier, "12345")
            };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var user = new CurrentUser(claimsPrincipal);
            var userId = user.Id;

            user.Id.Should().Be(12345);
            userId.GetType().Should().Be(typeof(long));
        }
    }
}
