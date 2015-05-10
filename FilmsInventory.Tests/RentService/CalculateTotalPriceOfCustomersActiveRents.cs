using FilmsInventory.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmsInventory.Tests.RentService
{
    [TestClass]
    public class CalculateTotalPriceOfCustomersActiveRents: TestCase
    {
        [TestMethod]
        public void SuccessfulCalculationOfTotalPriceOfCustomersActiveRents()
        {
            var igor = CreateIgorCustomer();
            var matrix = CreateMatrixFilm();
            var zombieland = CreateZombielandFilm();

            rentService.RentFilmByCustomer(matrix.Name, igor.Name, RentData.TwoDays);
            rentService.RentFilmByCustomer(zombieland.Name, igor.Name, RentData.FiveDays);

            var totalPrice = rentService.CalculateTotalPriceOfCustomersActiveRents(igor.Name);

            var expectedPrice = RentData.NewReleasesPriceForTwoDays + RentData.RegularFilmsPriceForFiveDays;
            Assert.AreEqual(totalPrice, expectedPrice);
        }

        [TestMethod]
        public void CalculationOfTotalPriceIfCustomersHasNoActiveRents_ReturnsEmptyColeection()
        {
            var igor = CreateIgorCustomer();

            var totalPrice = rentService.CalculateTotalPriceOfCustomersActiveRents(igor.Name);
            
            Assert.AreEqual(totalPrice, 0);
        }
    }
}
