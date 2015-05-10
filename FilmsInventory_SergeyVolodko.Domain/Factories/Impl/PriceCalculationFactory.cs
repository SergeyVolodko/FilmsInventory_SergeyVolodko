using FilmsInventory.Calculations;
using FilmsInventory.Entities;

namespace FilmsInventory.Factories.Impl
{
    public class PriceCalculationFactory : IPriceCalculationFactory
    {
        public PriceCalculation CreateCalculation(FilmType filmType, int rentDaysCount)
        {
            switch (filmType)
            {
                case FilmType.NewReleases: return new NewReleasesCalculation(rentDaysCount);
                case FilmType.RegularFilms: return new RegularFilmsCalculation(rentDaysCount);
                case FilmType.OldFilms: return new OldFilmsCalculation(rentDaysCount);
            }

            return new RegularFilmsCalculation(rentDaysCount);
        }
    }
}
