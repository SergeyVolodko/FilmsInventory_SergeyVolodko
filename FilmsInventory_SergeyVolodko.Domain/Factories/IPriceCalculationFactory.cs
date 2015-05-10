
using FilmsInventory.Calculations;
using FilmsInventory.Entities;

namespace FilmsInventory.Factories
{
    public interface IPriceCalculationFactory
    {
        PriceCalculation CreateCalculation(FilmType filmType, int rentDaysCount);
    }
}
