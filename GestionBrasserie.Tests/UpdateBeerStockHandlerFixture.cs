using AutoFixture.Xunit2;
using FluentAssertions;
using GestionBrasserie.Domain;
using GestionBrasserie.Services.Brewery;
using GestionBrasserie.Services.Reseller;
using GestionBrasserie.Test;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GestionBrasserie.Tests
{
    public class UpdateBeerStockHandlerFixture
    {
        [Theory]
        [AutoServiceData]
        public async Task UpdateBeerStockTest(UpdateBeerStockHandler sut,UpdateBeerStockRequest request, Reseller expected,CancellationToken token)
        {
            //Setup
            var brewery = new Brewery("BreweryTest");
            var beer = new Beer(brewery, "BeerTest", 2, 3);
            beer.Id = request.BeerId;
            request.Id = beer.Id;
            expected.AddListedBeer(beer);
            sut.ResellerRepository.AsMock()
          .Setup(c => c.GetByIdAsync(request.Id, token)).ReturnsAsync(expected);
            //Act
            var actual = await sut.Handle(request, token);
            //Assert
            expected.BeersStock.First(x => x.Beer.Id == request.BeerId && x.Quantity == request.Quantity).Should().NotBeNull();
        }
    }
   
}