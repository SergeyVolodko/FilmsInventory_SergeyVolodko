using System;
using System.Linq;
using FilmsInventory.Exceptions;
using FilmsInventory.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmsInventory.Tests.RentService
{
    [TestClass]
    public class RentFilmByCustomer: TestCase
    {
        [TestMethod]
        public void SuccessfulStartRentByCustomer()
        {
            var igor = CreateIgorCustomer();
            var matrix = CreateMatrixFilm();

            rentService.RentFilmByCustomer(matrix.Name, igor.Name, RentData.TwoDays);

            var igorsRentals = rentService.GetCustomersActiveRents(igor.Name);

            Assert.AreEqual(igorsRentals.Count, 1);

            var rentedFilmsNames = igorsRentals.Select(r => r.FilmName).ToList();
            CollectionAssert.Contains(rentedFilmsNames, matrix.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredFieldNotSpecified))]
        public void EmptyFilmName_ThrowsException()
        {
            var igor = CreateIgorCustomer();
            rentService.RentFilmByCustomer(String.Empty, igor.Name, RentData.TwoDays);
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredFieldNotSpecified))]
        public void NullFilmName_ThrowsException()
        {
            var igor = CreateIgorCustomer();
            rentService.RentFilmByCustomer(null, igor.Name, RentData.TwoDays);
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredFieldNotSpecified))]
        public void EmptyCustomerName_ThrowsException()
        {
            var matrix = CreateMatrixFilm();
            rentService.RentFilmByCustomer(matrix.Name, String.Empty, RentData.TwoDays);
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredFieldNotSpecified))]
        public void NullCustomerName_ThrowsException()
        {
            var matrix = CreateMatrixFilm();
            rentService.RentFilmByCustomer(matrix.Name, null, RentData.TwoDays);
        }

        [TestMethod]
        [ExpectedException(typeof(FilmWithSpecifiedNameDoesNotExistException))]
        public void NotExistingFilmName_ThrowsException()
        {
            var matrix = CreateMatrixFilm();
            var igor = CreateIgorCustomer();

            rentService.RentFilmByCustomer(FilmData.ZombielandName, igor.Name, RentData.TwoDays);
        }
    }
}
