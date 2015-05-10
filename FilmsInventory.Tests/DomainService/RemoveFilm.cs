using System;
using FilmsInventory.Entities;
using FilmsInventory.Exceptions;
using FilmsInventory.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmsInventory.Tests.DomainService
{
    [TestClass]
    public class RemoveFilm: TestCase
    {
        [TestMethod]
        public void SuccessfullFilmRemovingByName()
        {
            var matrix = domainService.CreateFilm(FilmData.TheMatrixName, FilmType.NewReleases);
            var godfather = domainService.CreateFilm(FilmData.GodfatherName, FilmType.OldFilms);
            
            var fetchedFilms = domainService.GetAllFilms();
            Assert.AreEqual(fetchedFilms.Count, 2);

            domainService.RemoveFilm(FilmData.TheMatrixName);

            fetchedFilms = domainService.GetAllFilms();
            Assert.AreEqual(fetchedFilms.Count, 1);

            CollectionAssert.DoesNotContain(fetchedFilms, matrix);
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredFieldNotSpecified))]
        public void EmptyFilmName_ThrowsException()
        {
            CreateMatrixFilm();
            domainService.RemoveFilm(String.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(RequiredFieldNotSpecified))]
        public void NullFilmName_ThrowsException()
        {
            CreateMatrixFilm();
            domainService.RemoveFilm(null);
        }

        [TestMethod]
        [ExpectedException(typeof (FilmWithSpecifiedNameDoesNotExistException))]
        public void WrongFilmName_ThrowsException()
        {
            CreateMatrixFilm();
            domainService.RemoveFilm(FilmData.ZombielandName);
        }
    }
}
