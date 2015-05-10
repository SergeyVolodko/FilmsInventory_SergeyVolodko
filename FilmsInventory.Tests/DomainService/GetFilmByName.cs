using System;
using FilmsInventory.Entities;
using FilmsInventory.Exceptions;
using FilmsInventory.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmsInventory.Tests.DomainService
{
    [TestClass]
    public class GetFilmByName: TestCase
    {
        [TestMethod]
        public void SuccessfullGetFilmByName()
        {
            var theMatrix = domainService.CreateFilm(FilmData.TheMatrixName, FilmType.NewReleases);
            var fetchedFilm = domainService.GetFilmByName(FilmData.TheMatrixName);
            
            Assert.IsNotNull(fetchedFilm);
            Assert.AreEqual(fetchedFilm, theMatrix);
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredFieldNotSpecified))]
        public void EmptyFilmName_ThrowsException()
        {
            var theFilm = domainService.GetFilmByName(String.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredFieldNotSpecified))]
        public void NullFilmName_ThrowsException()
        {
            var theFilm = domainService.GetFilmByName(null);
        }

        [TestMethod]
        public void NotExistingFilmName_ReturnsNull()
        {
            var theFilm = domainService.GetFilmByName(FilmData.TheMatrixName);
            Assert.IsNull(theFilm);
        }
    }
}
