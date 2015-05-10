using FilmsInventory.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmsInventory.Tests.RentService
{
    [TestClass]
    public class FilmIsNotAvailable: TestCase
    {
        [TestMethod]
        public void RentedFilmIsNotAvailable()
        {
            var igor = CreateIgorCustomer();
            var matrix = CreateMatrixFilm();
            var zombieland = CreateZombielandFilm();

            rentService.RentFilmByCustomer(matrix.Name, igor.Name, RentData.TwoDays);

            var matrixFilmIsNotAvailble = rentService.FilmIsNotAvailable(matrix.Name);
            var zombielandFilmIsNotAvailble = rentService.FilmIsNotAvailable(zombieland.Name);

            Assert.IsTrue(matrixFilmIsNotAvailble);
            Assert.IsFalse(zombielandFilmIsNotAvailble);
        }

        [TestMethod]
        public void NotRentedFilmIsAvailable()
        {
            var igor = CreateIgorCustomer();
            var matrix = CreateMatrixFilm();

            var filmIsNotAvailble = rentService.FilmIsNotAvailable(matrix.Name);

            Assert.IsFalse(filmIsNotAvailble);
        }
    }
}
