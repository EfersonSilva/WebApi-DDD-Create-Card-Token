using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WebApi.Application.CreateToken;
using WebApi.Application.Interfaces;
using WebApi.Application.Query.CheckToken;
using WebApi.Application.Validator;
using WebApi.Domain.Entity;
using Xunit;
using System.Linq;

namespace WebApi.Tests.Application
{
    public class CheckTokenTests
    {
        private readonly Mock<ICardRepository> _cardRepository;
        private readonly Mock<ICreateToken> _createToken;
        private readonly ICheckTokenApplication _checkTokenApplication;

        public CheckTokenTests()
        {
            _cardRepository = new Mock<ICardRepository>();
            _createToken = new Mock<ICreateToken>();
            _checkTokenApplication = new CheckTokenApplication(_createToken.Object, _cardRepository.Object, new ValidatorToken());
        }

        [Fact]
        public async Task Not_Valid_If_Token_Doesnt_Match()
        {
            //Arrange
            var tokenValidator = Faker.CheckTokenFaker.ChekedTokenRequest().Generate();

            _createToken.Setup(x =>
             x.CreatTokenAsync(It.IsAny<CreateTokenRequest>())).Returns(99999);

            var card = new Card(99999999, tokenValidator.CustomerId);

            _cardRepository.Setup(x => x.FindCardAsync(tokenValidator.CardId)).Returns(Task.FromResult(card));

            //Act
            var validation = await _checkTokenApplication.CheckToken(tokenValidator);

            //Assert
            Assert.False(validation);
        }

        [Fact]
        public async Task Should_Return_Not_Valid_If_Card_Null()
        {
            //Arrange
            var tokenValidator = Faker.CheckTokenFaker.ChekedTokenRequest().Generate();

            _createToken.Setup(x =>
            x.CreatTokenAsync(It.IsAny<CreateTokenRequest>())).Returns(99999);

            _cardRepository.Setup(x => x.FindCardAsync(9)).Returns(Task.FromResult((Card)null));

            //Act & Asset
            await Assert.ThrowsAnyAsync<Exception>(() => _checkTokenApplication.CheckToken(tokenValidator));
        }

        [Fact]
        public async Task Should_Return_Valid_If_Card_Is_Valid()
        {
            //Arrange
            var tokenValidator = Faker.CheckTokenFaker.ChekedTokenRequest().Generate();

            _createToken.Setup(x =>
            x.CreatTokenAsync(It.IsAny<CreateTokenRequest>())).Returns(tokenValidator.Token);

            var card = new Card()
            {
                CardId = tokenValidator.CardId,
                CardNumber = 9999,
                CustomerId = tokenValidator.CustomerId,
                CreationAt = DateTime.Now.AddMinutes(-10),
                Excluded = false
            };

            _cardRepository.Setup(x => x.FindCardAsync(tokenValidator.CardId)).Returns(Task.FromResult(card));

            //Act
            var validation = await _checkTokenApplication.CheckToken(tokenValidator);

            //Asset
            Assert.True(validation);
        }

        [Fact]
        public async Task Should_Return_Invalided_If_more_30_minuts()
        {
            //Arrange
            var tokenValidator = Faker.CheckTokenFaker.ChekedTokenRequest().Generate();

            _createToken.Setup(x =>
            x.CreatTokenAsync(It.IsAny<CreateTokenRequest>())).Returns(tokenValidator.Token);

            var card = new Card()
            {
                CardId = tokenValidator.CardId,
                CardNumber = 9999,
                CustomerId = tokenValidator.CustomerId,
                CreationAt = DateTime.Now.AddMinutes(-31),
                Excluded = false
            };

            _cardRepository.Setup(x => x.FindCardAsync(tokenValidator.CardId)).Returns(Task.FromResult(card));

            //Act
            var validation = await _checkTokenApplication.CheckToken(tokenValidator);

            //Asset
            Assert.False(validation);
        }

        [Fact]
        public async Task Should_Return_Exception()
        {
            //Arrange
            var tokenRequest = new CheckTokenRequest();

            //ACT & Assert
            await Assert.ThrowsAnyAsync<Exception>(() => _checkTokenApplication.CheckToken(tokenRequest));
        }

    }
}
