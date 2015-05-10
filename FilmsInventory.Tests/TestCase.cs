
using System;
using FilmsInventory.Entities;
using FilmsInventory.Factories;
using FilmsInventory.Factories.Impl;
using FilmsInventory.Repositories;
using FilmsInventory.Services;
using FilmsInventory.Tests.Data;
using FilmsInventory.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CoreDomainService = FilmsInventory.Services.DomainService;

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
            var entityFactory = new EntityFactory();
            var priceCalculationFactory = new PriceCalculationFactory();

            var customerRepository = new InMemoryCustomerRepository();
            var filmRepository = new InMemoryFilmRepository();
            var rentRepository = new InMemoryRentRepository();

            mockTimeProvider = new Mock<TimeProvider>();
            mockTimeProvider.Setup(t => t.UtcNow).Returns(new DateTime(2015, 1, 1));

            this.domainService = new CoreDomainService(entityFactory, filmRepository, customerRepository);

            this.rentService = new Services.RentService(entityFactory, priceCalculationFactory, 
                                                        rentRepository, filmRepository, customerRepository,
                                                        mockTimeProvider.Object);
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
