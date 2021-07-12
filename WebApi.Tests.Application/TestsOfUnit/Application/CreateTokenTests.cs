using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApi.Application.CreateToken;
using WebApi.Application.Interfaces;
using WebApi.Tests.Faker;
using Xunit;

namespace WebApi.Tests.Application
{
    public class CheckTokenApplicationTests
    {
        private readonly ICreateToken _createToken;

        public CheckTokenApplicationTests()
        {
            _createToken = new CreateToken();
        }

        [Fact]
        public void should_checkType_Token()
        {
            //Arrange
            var createToken = CreateTokenFaker.CreateTokenRequest().Generate();

            //Act
            var token =  _createToken.CreatTokenAsync(createToken);

            //Assert
            Assert.IsType<long>(token);
        }

        [Fact]
        public void should_Exception_token_Null()
        {
            //Arrange
            var createToken = new CreateTokenRequest() {
                CardNumber = 0,
                Cvv = 0
            };

            //Act
            var error = Assert.Throws<Exception>(() => _createToken.CreatTokenAsync(createToken));

            //Assert
            Assert.Equal("Card Number or Cvv invalid.", error.Message);
        }

        [Theory]
        [InlineData(9999999999999999, 999)]
        [InlineData(8888888888888888, 8888)]
        [InlineData(1234567891234567, 1234)]
        [InlineData(9876543219876543, 4321)]
        [InlineData(4561237896541237, 2233)]
        [InlineData(7845128956237946, 8899)]
        public void Should_Return_Token_Renegerated(long cardNumber, int cvv)
        {
            //Arrange
            CreateTokenRequest createToken = new CreateTokenRequest(cardNumber, cvv);

            //Acct
            var token = _createToken.CreatTokenAsync(createToken);

            //Assert
            Assert.DoesNotMatch("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", token.ToString());
        }

        [Fact]
        public void MustBe_TwoEqual_Tokens()
        {
            //Arrange
            CreateTokenRequest TokenOne = new CreateTokenRequest(9999999999999999, 9999);
            CreateTokenRequest TokenTwo = new CreateTokenRequest(9999999999999999, 9999);

            //Acct
            var tokenOne = _createToken.CreatTokenAsync(TokenOne);
            var tokenTwo = _createToken.CreatTokenAsync(TokenTwo);

            //Assert
            Assert.True(tokenOne == tokenTwo);
        }
    }
}
