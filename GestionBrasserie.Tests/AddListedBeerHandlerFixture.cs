using AutoFixture.Xunit2;
using FluentAssertions;
using GestionBrasserie.Domain;
using GestionBrasserie.Services.Brewery;
using GestionBrasserie.Services.Reseller;
using GestionBrasserie.Test;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GestionBrasserie.Tests
{
    public class AddListedBeerHandlerFixture
    {
        [Theory]
        [AutoServiceData]
        public async Task AddListedBeerHandlerTest(AddListedBeerHandler sut, AddListedBeerRequest request, Reseller expectedReseller, CancellationToken token)
        {
            //Setup
            var brewery = new Brewery("BreweryTest");
            var expectedBeer = new Beer(brewery, "BeerTest", 2, 4);
            sut.ResellerRepository.AsMock()
            .Setup(c => c.GetByIdAsync(request.ResellerId, token)).ReturnsAsync(expectedReseller);
            sut.BeersRepository.AsMock()
           .Setup(c => c.GetByIdAsync(request.BeerId, token)).ReturnsAsync(expectedBeer);
            //Act
            var actual = await sut.Handle(request, token);
            //Assert
            expectedReseller.BeersStock.First(x => x.Beer.Id == expectedBeer.Id).Should().NotBeNull();

        }
        [Theory]
        [AutoServiceData]
        public async Task AddListedBeer_WithSameId_ShouldThrow(AddListedBeerHandler sut, AddListedBeerRequest request, Reseller expectedReseller, CancellationToken token)
        {
            //Setup
            var brewery = new Brewery("BreweryTest");
            var expectedBeer = new Beer(brewery, "BeerTest", 2, 4);
            expectedReseller.AddListedBeer(expectedBeer);
             request.BeerId = expectedReseller.BeersStock.First().Beer.Id;
            sut.ResellerRepository.AsMock()
            .Setup(c => c.GetByIdAsync(request.ResellerId, token)).ReturnsAsync(expectedReseller);
            //Act
            var act = () => sut.Handle(request, token);
            //Assert
            await act.Should().ThrowAsync<InvalidOperationException>();

        }
    }

}