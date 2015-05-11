using FilmsInventory.Factories;
using FilmsInventory.Factories.Impl;
using FilmsInventory.Repositories;
using FilmsInventory.Utils;
using Microsoft.Practices.Unity;

namespace FilmsInventory.Tests
{
    public static class Bootstrapper
    {
        public static void Configure(IUnityContainer container)
        {
            container.RegisterType(typeof(IEntityFactory), typeof(EntityFactory));
            container.RegisterType(typeof(IPriceCalculationFactory), typeof(PriceCalculationFactory));

            container.RegisterInstance(typeof(IFilmRepository), new InMemoryFilmRepository());
            container.RegisterInstance(typeof(ICustomerRepository), new InMemoryCustomerRepository());
            container.RegisterInstance(typeof(IRentRepository), new InMemoryRentRepository());

            container.RegisterInstance(typeof (TimeProvider), new TestTimeProvider());
        }
    }
}
