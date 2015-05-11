
using FilmsInventory.Entities;
using FilmsInventory.Services;
using FilmsInventory.Tests.Data;
using FilmsInventory.Utils;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FilmsInventory.Tests
{
    [TestClass]
    public class TestCase
    {
        protected IDomainService domainService;
        protected IRentService rentService;

        protected Mock<TimeProvider> mockTimeProvider;

        [TestInitialize]
        public void SetupFixure()
        {
            var container = new UnityContainer();
            Bootstrapper.Configure(container);

            this.domainService = container.Resolve<Services.DomainService>();
            this.rentService = container.Resolve<Services.RentService>();
        }

        protected Film CreateMatrixFilm()
        {
            return domainService.CreateFilm(FilmData.TheMatrixName, FilmType.NewReleases);
        }

        protected Film CreateZombielandFilm()
        {
            return domainService.CreateFilm(FilmData.ZombielandName, FilmType.RegularFilms);
        }

        protected Customer CreateIgorCustomer()
        {
            return domainService.CreateCustomer(CustomerData.IgorName);
        }
    }
}
