using FilmsInventory.Entities;
using FilmsInventory.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmsInventory.Tests.RentService
{
    [TestClass]
    public class AssignBonusRentPayment: TestCase
    {
        [TestMethod]
        public void PaymentForTwoDaysRentOfNewReleaseFilm_Is50()
        {
            var igor = CreateIgorCustomer();
            var matrix = CreateMatrixFilm();

            igor.AddBonusPoints(RentData.NewReleaseBonusPointsPriceForTwoDays + 1);
            var rentTerm = RentData.TwoDays;

            var payment = rentService.CreateBonusesRentPayment(FilmType.NewReleases, rentTerm);
            var rent = rentService.RentFilmByCustomer(matrix.Name, igor.Name, rentTerm);
            rent = rentService.AssignBonusRentPayment(igor.Name, rent.Id, payment);

            var price = RentData.NewReleaseBonusPointsPriceForTwoDays;

            Assert.AreEqual(rent.Payment.Cost, price);
            Assert.AreEqual(rent.Payment.Currency, Currency.BonusPoints);

            Assert.AreEqual(igor.BonusPoints, 1);
        }
    }
}
