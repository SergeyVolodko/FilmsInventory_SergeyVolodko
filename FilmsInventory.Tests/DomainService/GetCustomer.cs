using System;
using FilmsInventory.Exceptions;
using FilmsInventory.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmsInventory.Tests.DomainService
{
    [TestClass]
    public class GetCustomer: TestCase
    {
        [TestMethod]
        public void SuccessfullCustomerGet()
        {
            var igorCustomer = CreateIgorCustomer();

            var fetchedCustomer = domainService.GetCustomer(CustomerData.IgorName);

            Assert.IsNotNull(fetchedCustomer);
            Assert.AreEqual(fetchedCustomer, igorCustomer);
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredFieldNotSpecified))]
        public void EmptyFilmName_ThrowsException()
        {
            CreateIgorCustomer();
            var customer = domainService.GetCustomer(String.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredFieldNotSpecified))]
        public void NullFilmName_ThrowsException()
        {
            CreateIgorCustomer();
            var customer = domainService.GetCustomer(null);
        }

        [TestMethod]
        public void NotExistingFilmName_ReturnsNull()
        {
            var customer = domainService.GetFilmByName(CustomerData.NicolasName);
            Assert.IsNull(customer);
        }
    }
}
