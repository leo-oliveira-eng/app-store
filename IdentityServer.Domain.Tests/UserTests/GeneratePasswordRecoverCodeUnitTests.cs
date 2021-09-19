using FluentAssertions;
using IdentityServer.Domain.Tests.Shared;
using System;
using Xunit;

namespace IdentityServer.Domain.Tests.UserTests
{
    public class GeneratePasswordRecoverCodeUnitTests : BaseMock
    {
        [Fact]
        public void GeneratePasswordRecoverCode_ShouldAddRecoverCode()
        {
            var user = UserFake();

            user.GeneratePasswordRecoverCode();

            user.PasswordRecoverCode.Should().NotBeNull();
            user.RecoverPasswordExpirationDate.Should().NotBeNull();
            user.RecoverPasswordExpirationDate.Value.Date.Should().Be(DateTime.Today);
        }
    }
}
