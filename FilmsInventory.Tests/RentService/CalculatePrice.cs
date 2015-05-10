using FilmsInventory.Entities;
using FilmsInventory.Exceptions;
using FilmsInventory.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmsInventory.Tests.RentService
{
    [TestClass]
    public class CalculatePrice: TestCase
    {
        [TestMethod]
        public void NewReleasesCalculationForTwoDays_Returns80()
        {
            var igor = CreateIgorCustomer();
            var zombieland = domainService.CreateFilm(FilmData.ZombielandName, FilmType.NewReleases);
            var rent = rentService.RentFilmByCustomer(zombieland.Name, igor.Name, RentData.TwoDays);

            var price = rentService.CalculatePrice(zombieland.Type, rent.DaysCount);

            Assert.AreEqual(price, RentData.NewReleasesPriceForTwoDays);
        }

        [TestMethod]
        public void RegularFilmsCalculationForTwoDays_Returns30()
        {
            var igor = CreateIgorCustomer();
            var matrix = domainService.CreateFilm(FilmData.TheMatrixName, FilmType.RegularFilms);
            var rent = rentService.RentFilmByCustomer(matrix.Name, igor.Name, RentData.TwoDays);

            var price = rentService.CalculatePrice(matrix.Type, rent.DaysCount);

            Assert.AreEqual(price, RentData.RegularFilmsPriceForTwoDays);
        }

        [TestMethod]
        public void RegularFilmsCalculationForFiveDays_Returns90()
        {
            var igor = CreateIgorCustomer();
            var matrix = domainService.CreateFilm(FilmData.TheMatrixName, FilmType.RegularFilms);
            var rent = rentService.RentFilmByCustomer(matrix.Name, igor.Name, RentData.FiveDays);

            var price = rentService.CalculatePrice(matrix.Type, rent.DaysCount);

            Assert.AreEqual(price, RentData.RegularFilmsPriceForFiveDays);
        }

        [TestMethod]
        public void OldFilmsCalculationForTwoDays_Returns30()
        {
            var igor = CreateIgorCustomer();
            var godfather = domainService.CreateFilm(FilmData.GodfatherName, FilmType.OldFilms);
            var rent = rentService.RentFilmByCustomer(godfather.Name, igor.Name, RentData.TwoDays);

            var price = rentService.CalculatePrice(godfather.Type, rent.DaysCount);

            Assert.AreEqual(price, RentData.OldFilmsPriceForTwoDays);
        }

        [TestMethod]
        public void OldFilmsCalculationForevenDays_Returns90()
        {
            var igor = CreateIgorCustomer();
            var godfather = domainService.CreateFilm(FilmData.GodfatherName, FilmType.OldFilms);
            var rent = rentService.RentFilmByCustomer(godfather.Name, igor.Name, RentData.SevenDays);

            var price = rentService.CalculatePrice(godfather.Type, rent.DaysCount);

            Assert.AreEqual(price, RentData.OldFilmsPriceForSevenDays);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongDaysCountException))]
        public void NegativeRentDays_ThrowsException()
        {
            var matrix = CreateMatrixFilm();
            var price = rentService.CalculatePrice(matrix.Type, RentData.NegativeDay);
        }
    }
}
