using FilmsInventory.Entities;
using FilmsInventory.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmsInventory.Tests.DomainService
{
    [TestClass]
    public class GainBonusPoints: TestCase
    {
        [TestMethod]
        public void CustomerGainsTwoBonusPointsForRentingNewReleaseFilm()
        {
            var igor = CreateIgorCustomer();
            var matrix = CreateMatrixFilm();

            rentService.RentFilmByCustomer(matrix.Name, igor.Name, RentData.TwoDays);
            igor = domainService.GainBonusPoints(igor.Name, matrix.Type);

            var bonusPoints = igor.BonusPoints;

            Assert.AreEqual(bonusPoints, 2);
        }

        [TestMethod]
        public void CustomerGainsOneBonusPointForRentingOldOrRegularFilm()
        {
            var igor = CreateIgorCustomer();
            var matrix = domainService.CreateFilm(FilmData.TheMatrixName, FilmType.RegularFilms);
            var godfather = domainService.CreateFilm(FilmData.GodfatherName, FilmType.OldFilms);

            rentService.RentFilmByCustomer(matrix.Name, igor.Name, RentData.TwoDays);
            rentService.RentFilmByCustomer(godfather.Name, igor.Name, RentData.TwoDays);

            igor = domainService.GainBonusPoints(igor.Name, matrix.Type);
            igor = domainService.GainBonusPoints(igor.Name, godfather.Type);

            var bonusPoints = igor.BonusPoints;

            Assert.AreEqual(bonusPoints, 2);
        }
    }
}
