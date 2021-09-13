using DonVo.MessagingService.API.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Xunit;

namespace DonVo.MessagingService.API.xUnitTest
{
    public class JwtServiceTest
    {

        private readonly JwtService jwtService;

        public JwtServiceTest()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"Security:JwtSecret", Guid.NewGuid().ToString()},
                {"Security:TokenExpireIn", new Random().Next(5, 10).ToString()},
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            jwtService = new JwtService(configuration);
        }


        [Fact]
        public void CreateToken_Succesfull()
        {
            Dictionary<string, string> claims = new();
            var result = jwtService.CreateToken(claims);
            Assert.NotNull(result);
            Assert.Equal(3, result.Split('.').Length);

        }

        [Fact]
        public void CreateToken_Fail_NullClaims()
        {
            Dictionary<string, string> claims = null;
            Assert.Throws<System.ArgumentNullException>(() => jwtService.CreateToken(claims));
        }


    }
}