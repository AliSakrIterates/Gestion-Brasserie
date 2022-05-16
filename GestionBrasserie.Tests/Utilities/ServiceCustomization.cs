using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using GestionBrasserie.Data;
using GestionBrasserie.Domain;
using System.Linq;

namespace GestionBrasserie.Tests
{
    public class ServiceCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize(
               new AutoMoqCustomization
               {
                   ConfigureMembers = true
               });
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            fixture.Customize<Beer>(c => c
            .With(x => x.AlcoholPercentage, 50));
          
        }
    }
}