
namespace FilmsInventory.Calculations
{
    public abstract class PriceCalculation
    {
        protected readonly int rentDaysCount;
        protected PriceCalculation(int rentDaysCount)
        {
            this.rentDaysCount = rentDaysCount;
        }

        public abstract int Perform();
    }
}
