using FilmsInventory.Entities;
using FilmsInventory.Exceptions;
using FilmsInventory.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmsInventory.Tests.RentService
{
    [TestClass]
    public class CreateBonusesRentPayment: TestCase
    {
        [TestMethod]
        public void SuccessfullBonusesPaymentCreation()
        {
            var matrix = CreateMatrixFilm();

            var rentTerm = RentData.TwoDays;

            var payment = rentService.CreateBonusesRentPayment(matrix.Type, rentTerm);

            var price = RentData.NewReleaseBonusPointsPriceForTwoDays;

            Assert.AreEqual(payment.Cost, price);
            Assert.AreEqual(payment.Currency, Currency.BonusPoints);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongDaysCountException))]
        public void NegativeRentDaysCount_ThrowsException()
        {
            var payment = rentService.CreateBonusesRentPayment(FilmType.NewReleases, RentData.NegativeDay);
        }

        [TestMethod]
        [ExpectedException(typeof(CannotPayWithBonusesForNonNewRelaeseFilmsException))]
        public void WrongFilmType_ThrowsException()
        {
            rentService.CreateBonusesRentPayment(FilmType.OldFilms, RentData.TwoDays);
        }
    }
}
