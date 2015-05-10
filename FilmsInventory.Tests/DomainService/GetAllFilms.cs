using FilmsInventory.Entities;
using FilmsInventory.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FilmsInventory.Tests.DomainService
{
    [TestClass]
    public class GetAllFilms: TestCase
    {
        [TestMethod]
        public void SuccessfullGetAllFilms()
        {
            var matrix = domainService.CreateFilm(FilmData.TheMatrixName, FilmType.NewReleases);
            var godfather = domainService.CreateFilm(FilmData.GodfatherName, FilmType.OldFilms);
            var zombieland = domainService.CreateFilm(FilmData.ZombielandName, FilmType.OldFilms);
            var fetchedFilms = domainService.GetAllFilms();

            Assert.AreEqual(fetchedFilms.Count, 3);
            CollectionAssert.Contains(fetchedFilms, matrix);
            CollectionAssert.Contains(fetchedFilms, godfather);
            CollectionAssert.Contains(fetchedFilms, zombieland);
        }

        [TestMethod]
        public void GetAllFilmsIfNoFilmsStored_ReturnsEmptyCollection()
        {
            var fetchedFilms = domainService.GetAllFilms();

            Assert.AreEqual(fetchedFilms.Count, 0);
        }
    }
}
