using FilmsInventory.Entities;
using FilmsInventory.Exceptions;
using FilmsInventory.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmsInventory.Tests.RentService
{
    [TestClass]
    public class AssignEURRentPayment: TestCase
    {
        [TestMethod]
        public void SuccessfullPaymentRentAssignment()
        {
            var igor = CreateIgorCustomer();
            var matrix = CreateMatrixFilm();

            var rentTerm = RentData.TwoDays;

            var payment = rentService.CreateEURRentPayment(matrix.Type, rentTerm);
            var rent = rentService.RentFilmByCustomer(matrix.Name, igor.Name, rentTerm);
            rent = rentService.AssignEURRentPayment(rent.Id, payment);

            var price = rentService.CalculatePrice(matrix.Type, rentTerm);

            Assert.AreEqual(rent.Payment.Cost, price);
            Assert.AreEqual(rent.Payment.Currency, Currency.EUR);
        }

        [TestMethod]
        [ExpectedException(typeof(NotExistingRentException))]
        public void NotExistingRentId_ThorwsException()
        {
            var payment = rentService.CreateEURRentPayment(FilmType.OldFilms, RentData.TwoDays);
            rentService.AssignEURRentPayment(1, payment);
        }
    }
}
