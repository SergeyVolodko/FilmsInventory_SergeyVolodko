using FilmsInventory.Tests.Data;
using FilmsInventory.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmsInventory.Tests.RentService
{
    [TestClass]
    public class GetAvailableFilms: TestCase
    {
        [TestMethod]
        public void SuccessfulGetAvailableFilmsIfNoFilmsIsRented()
        {
            var igor = CreateIgorCustomer();
            var matrix = CreateMatrixFilm();
            var zombieland = CreateZombielandFilm();

            var availableFilms = rentService.GetAvailableFilms();

            CollectionAssert.Contains(availableFilms, matrix);
            CollectionAssert.Contains(availableFilms, zombieland);
        }

        [TestMethod]
        public void SuccessfulGetAvailableFilmsIfSomeFilmsAreRented()
        {
            var igor = CreateIgorCustomer();
            var matrix = CreateMatrixFilm();
            var zombieland = CreateZombielandFilm();

            rentService.RentFilmByCustomer(matrix.Name, igor.Name, RentData.TwoDays);
            var availableFilms = rentService.GetAvailableFilms();
            
            CollectionAssert.Contains(availableFilms, zombieland);
            CollectionAssert.DoesNotContain(availableFilms, matrix);
        }

        [TestCleanup]
        public void TearDown()
        {
            TimeProvider.ResetToDefault();
        }
    }
}
