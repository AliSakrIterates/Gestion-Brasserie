using AutoFixture.Xunit2;
using FluentAssertions;
using GestionBrasserie.Domain;
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
    public class GetQuotationHandlerFixture
    {
        [Theory]
        [AutoServiceData]
        public async Task GetQuotation_WhenQuantityBiggerThan10_ShouldDiscount(GetQuotationHandler sut,GetQuotationRequest request, Reseller expected,CancellationToken token)
        {
            //Setup
            var brewery = new Brewery("testBrewery");
            var beer = new Beer(brewery, "testBeer", 2, 3);
            expected.BeersStock.Clear();
            expected.AddListedBeer(beer);
            expected.BeersStock.Single().Quantity = 20;
            request.Beers.Clear();
            request.Beers.Add(new BeersQuotationRequest() { BeerId = beer.Id, Quantity = 15 });
            sut.ResellerRepository.AsMock()
          .Setup(c => c.GetByIdAsync(request.ResellerId, token)).ReturnsAsync(expected);
            //Act
            var actual = await sut.Handle(request, token);
            //Assert
            actual.Price.Should().Be(beer.Price * 15 / (decimal)1.1);
            
        }

        [Theory]
        [AutoServiceData]
        public async Task GetQuotation_WhenQuantityBiggerStock_ShouldThrow(GetQuotationHandler sut, GetQuotationRequest request, Reseller expected, CancellationToken token)
        {
            //Setup
            var brewery = new Brewery("testBrewery");
            var beer = new Beer(brewery, "testBeer", 2, 3);
            expected.BeersStock.Clear();
            expected.AddListedBeer(beer);
            expected.BeersStock.Single().Quantity = 20;
            request.Beers.Clear();
            request.Beers.Add(new BeersQuotationRequest() { BeerId = beer.Id, Quantity = 30 });
            sut.ResellerRepository.AsMock()
          .Setup(c => c.GetByIdAsync(request.ResellerId, token)).ReturnsAsync(expected);
            //Act
            var act = () => sut.Handle(request, token);
            //Assert
            await act.Should().ThrowAsync<InvalidOperationException>();

        }
    }
   
}