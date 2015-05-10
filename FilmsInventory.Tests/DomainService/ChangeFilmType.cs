using System;
using FilmsInventory.Entities;
using FilmsInventory.Exceptions;
using FilmsInventory.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FilmsInventory.Tests.DomainService
{
    [TestClass]
    public class ChangeFilmType: TestCase
    {
        [TestMethod]
        public void SuccessfullFilmTypeChange()
        {
            var matrix = CreateMatrixFilm();

            matrix = domainService.ChangeFilmType(matrix.Name, FilmType.OldFilms);
            
            Assert.AreEqual(matrix.Type, FilmType.OldFilms);
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredFieldNotSpecified))]
        public void EmptyFilmName_ThrowsException()
        {
            CreateMatrixFilm();
            domainService.ChangeFilmType(String.Empty, It.IsAny<FilmType>());
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredFieldNotSpecified))]
        public void NullFilmName_ThrowsException()
        {
            CreateMatrixFilm();
            domainService.ChangeFilmType(null, It.IsAny<FilmType>());
        }

        [TestMethod]
        [ExpectedException(typeof(FilmWithSpecifiedNameDoesNotExistException))]
        public void WrongFilmName_ThrowsException()
        {
            CreateMatrixFilm();
            domainService.ChangeFilmType(FilmData.ZombielandName, It.IsAny<FilmType>());
        }
    }
}
