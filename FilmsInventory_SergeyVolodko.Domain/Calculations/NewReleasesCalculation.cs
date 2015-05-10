using FilmsInventory.Utils;

namespace FilmsInventory.Calculations
{
    public class NewReleasesCalculation: PriceCalculation
    {
        public NewReleasesCalculation(int rentDaysCount): base(rentDaysCount)
        {
        }

        public override int Perform()
        {
            var price = Consts.PREMIUM_FEE * rentDaysCount;
            return price;
        }
    }
}
