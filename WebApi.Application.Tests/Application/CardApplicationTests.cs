using System;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using WebApi.Application.Command;
using WebApi.Application.CreateToken;
using WebApi.Application.Interfaces;
using WebApi.Application.Validator;
using Xunit;

namespace WebApi.Tests.Application
{
    public class CardApplicationTests
    {
        private readonly Mock<ICardRepository> _cardRepository;
        private readonly Mock<ICreateToken> _createToken;
        private readonly ICardApplication _cardApplication;

        public CardApplicationTests()
        {
            _cardRepository = new Mock<ICardRepository>();
            _createToken = new Mock<ICreateToken>();
            _cardApplication = new CardApplication(new ValidatorCard(),_cardRepository.Object, _createToken.Object);
        }

        [Fact]
        public async Task Should_Save_Card_And_Valided()
        {
            //Arrange
            CardRequest cardRequest = Faker.CardApplicationFaker.CardRequest().Generate();

            _createToken.Setup(x => x.CreatTokenAsync(It.IsAny<CreateTokenRequest>()))
                .Returns(123456789);

            //Act
            CardResponse card = await _cardApplication.SaveCardAsync(cardRequest);

            //Assert
            //Nulls
            Assert.NotNull(card);
            Assert.True(card.CardId != 0);
            Assert.True(card.Token != 0);
            Assert.True(card.CreatedAt != null);
            //Type
            Assert.IsType<int>(card.CardId);
            Assert.IsType<DateTime>(card.CreatedAt);
            Assert.IsType<long>(card.Token);
        }

        [Theory]
        [InlineData(0,0,0)]
        [InlineData(999, 99999999999999999, 999)]
        [InlineData(999, 9999999999999999, 9999999)]
        public async Task Should_Return_ErrorsAsync(int customerId, long cardNumber, int cvv)
        {
            //Arrange
            CardRequest cardRequest = new CardRequest() {
            CardNumber = cardNumber,
            CustomerId= customerId,
            Cvv = cvv
            };

            //Act & Assert
            await Assert.ThrowsAnyAsync<Exception>(() => _cardApplication.SaveCardAsync(cardRequest));
        }
    }
}
