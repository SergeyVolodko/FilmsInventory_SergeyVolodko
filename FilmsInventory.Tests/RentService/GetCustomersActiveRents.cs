using System;
using System.Linq;
using FilmsInventory.Exceptions;
using FilmsInventory.Tests.Data;
using FilmsInventory.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmsInventory.Tests.RentService
{
    [TestClass]
    public class GetCustomersActiveRents : TestCase
    {
        [TestMethod]
        public void SuccessfulGetCustomersActiveRents()
        {
            var igor = CreateIgorCustomer();
            var matrix = CreateMatrixFilm();
            var zombieland = CreateZombielandFilm();

            rentService.RentFilmByCustomer(matrix.Name, igor.Name, RentData.TwoDays);
            rentService.RentFilmByCustomer(zombieland.Name, igor.Name, RentData.TwoDays);

            var customerRentals = rentService.GetCustomersActiveRents(igor.Name);

            Assert.AreEqual(customerRentals.Count, 2);

            var rentedFilmsNames = customerRentals.Select(r => r.FilmName).ToList();
            CollectionAssert.Contains(rentedFilmsNames, matrix.Name);
            CollectionAssert.Contains(rentedFilmsNames, zombieland.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredFieldNotSpecified))]
        public void EmptyCustomerName_ThrowsException()
        {
            var matrix = CreateMatrixFilm();
            var igor = CreateIgorCustomer();

            rentService.RentFilmByCustomer(matrix.Name, igor.Name, RentData.TwoDays);
            var igorsRentals = rentService.GetCustomersActiveRents(String.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredFieldNotSpecified))]
        public void NullCustomerName_ThrowsException()
        {
            var matrix = CreateMatrixFilm();
            var igor = CreateIgorCustomer();

            rentService.RentFilmByCustomer(matrix.Name, igor.Name, RentData.TwoDays);
            var igorsRentals = rentService.GetCustomersActiveRents(null);
        }

        [TestMethod]
        public void ExpieredRentTerm_ReturnsEmptyCollection()
        {
            var igor = CreateIgorCustomer();
            var zombieland = CreateZombielandFilm();
            
            rentService.RentFilmByCustomer(zombieland.Name, igor.Name, RentData.TwoDays);
            
            var privateObject = new PrivateObject(rentService);
            var timeProvider = (TestTimeProvider)privateObject.GetField("timeProvider");
            timeProvider.SetNowDate(new DateTime(2015, 3, 1));

            var customerRentals = rentService.GetCustomersActiveRents(igor.Name);

            Assert.AreEqual(customerRentals.Count, 0);
        }

        [TestCleanup]
        public void TearDown()
        {
            TimeProvider.ResetToDefault();
        }
    }
}
