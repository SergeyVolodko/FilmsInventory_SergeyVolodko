using System;
using FilmsInventory.Entities;
using FilmsInventory.Exceptions;
using FilmsInventory.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FilmsInventory.Tests.DomainService
{
    [TestClass]
    public class CreateFilm: TestCase
    {
        [TestMethod]
        public void SuccessfullFilmCreation()
        {
            var theMatrix = domainService.CreateFilm(FilmData.TheMatrixName, FilmType.NewReleases);

            var storedFilm = domainService.GetFilmByName(FilmData.TheMatrixName);

            Assert.AreEqual(theMatrix, storedFilm);
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredFieldNotSpecified))]
        public void EmptyFilmName_ThrowsException()
        {
            var theFilm = domainService.CreateFilm(String.Empty, It.IsAny<FilmType>());
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredFieldNotSpecified))]
        public void NullFilmName_ThrowsException()
        {
            var theFilm = domainService.CreateFilm(null, It.IsAny<FilmType>());
        }
    }
}
