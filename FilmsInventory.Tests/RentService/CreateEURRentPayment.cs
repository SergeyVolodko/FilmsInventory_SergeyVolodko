using FilmsInventory.Entities;
using FilmsInventory.Exceptions;
using FilmsInventory.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmsInventory.Tests.RentService
{
    [TestClass]
    public class CreateEURRentPayment: TestCase
    {
        [TestMethod]
        public void SuccessfullEURPaymentCreation()
        {
            var matrix = CreateMatrixFilm();

            var rentTerm = RentData.TwoDays;

            var payment = rentService.CreateEURRentPayment(matrix.Type, rentTerm);

            var price = rentService.CalculatePrice(matrix.Type, rentTerm);

            Assert.AreEqual(payment.Cost, price);
            Assert.AreEqual(payment.Currency, Currency.EUR);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongDaysCountException))]
        public void NegativeRentDaysCount_ThrowsException()
        {
            var payment = rentService.CreateEURRentPayment(FilmType.OldFilms, RentData.NegativeDay);
        }
    }
}
