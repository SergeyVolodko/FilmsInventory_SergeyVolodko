using FilmsInventory.Utils;

namespace FilmsInventory.Calculations
{
    public class RegularFilmsCalculation: PriceCalculation
    {
        public RegularFilmsCalculation(int rentDaysCount): base(rentDaysCount)
        {
        }

        public override int Perform()
        {
            int price = 0;
            if (rentDaysCount <= 3)
            {
                price = Consts.REGULAR_FEE;
            }
            else
            {
                var extraDays = rentDaysCount - 3;
                price = Consts.REGULAR_FEE + Consts.REGULAR_FEE * extraDays;
            }

            return price;
        }
    }
}
