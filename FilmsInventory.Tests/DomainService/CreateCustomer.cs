using System;
using FilmsInventory.Exceptions;
using FilmsInventory.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmsInventory.Tests.DomainService
{
    [TestClass]
    public class CreateCustomer: TestCase
    {
        [TestMethod]
        public void SuccessfullCustomerCreation()
        {
            var igor = domainService.CreateCustomer(CustomerData.IgorName);

            var fetchedCustomer = domainService.GetCustomer(CustomerData.IgorName);

            Assert.AreEqual(fetchedCustomer, igor);
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredFieldNotSpecified))]
        public void EmptyCustomerName_ThrowsException()
        {
            var customer = domainService.CreateCustomer(String.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredFieldNotSpecified))]
        public void NullCustomerName_ThrowsException()
        {
            var customer = domainService.CreateCustomer(null);
        }
    }
}
