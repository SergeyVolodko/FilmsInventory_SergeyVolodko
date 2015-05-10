using FilmsInventory.Entities;
using FilmsInventory.Exceptions;
using FilmsInventory.Tests.Data;
using FilmsInventory.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmsInventory.Tests.DomainService
{
    [TestClass]
    public class CanCustomerPayByBonuses: TestCase
    {
        [TestMethod]
        public void CustomerHasEnoughtBonusesToPayForRentTerm_ReturnsTrue()
        {
            var igor = CreateIgorCustomer();

            igor.AddBonusPoints(Consts.BONUSPOINTS_PRICE * 2);

            var customerCanPay = domainService.CanCustomerPayByBonuses(igor.Name, FilmType.NewReleases, RentData.TwoDays);

            Assert.IsTrue(customerCanPay);
        }

        [TestMethod]
        public void CustomerHasNotEnoughtBonusesToPayForRentTerm_ReturnsFalse()
        {
            var igor = CreateIgorCustomer();

            var customerCanPay = domainService.CanCustomerPayByBonuses(igor.Name, FilmType.NewReleases, RentData.TwoDays);

            Assert.IsFalse(customerCanPay);
        }

        [TestMethod]
        public void NonNewReleaseFilmType_ReturnsFalse()
        {
            var igor = CreateIgorCustomer();

            var customerCanPay = domainService.CanCustomerPayByBonuses(igor.Name, FilmType.OldFilms, RentData.TwoDays);

            Assert.IsFalse(customerCanPay);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongDaysCountException))]
        public void NegativeRentDays_ThrowsException()
        {
            var igor = CreateIgorCustomer();

            igor.AddBonusPoints(Consts.BONUSPOINTS_PRICE * 2);

            var customerCanPay = domainService.CanCustomerPayByBonuses(igor.Name, FilmType.NewReleases, RentData.NegativeDay);
        }
    }
}
