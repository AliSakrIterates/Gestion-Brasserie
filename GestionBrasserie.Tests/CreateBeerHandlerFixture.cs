using AutoFixture.Xunit2;
using FluentAssertions;
using GestionBrasserie.Domain;
using GestionBrasserie.Services.Brewery;
using GestionBrasserie.Test;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GestionBrasserie.Tests
{
    public class CreateBeerHandlerFixture
    {
        [Theory]
        [AutoServiceData]
        public async Task CreateBeerTest(CreateBeerHandler sut,CreateBeerRequest request,CancellationToken token)
        {
            //Setup
            var expected = new Brewery("BreweryTest");
            request.BreweryId = expected.Id;
            request.Beer.AlcoholPercentage = 50;

            sut.BreweryRepository.AsMock()
          .Setup(c => c.GetByIdAsync(request.BreweryId, token)).ReturnsAsync(expected);
            //Act
            var actual = await sut.Handle(request, token);
            //Assert
            expected.BrewedBeers.First(x=>x.Brewery.Id == request.BreweryId && x.AlcoholPercentage == request.Beer.AlcoholPercentage).Should().NotBeNull();

        }
    }
   
}