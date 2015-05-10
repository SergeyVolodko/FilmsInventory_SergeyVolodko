using FilmsInventory.Utils;

namespace FilmsInventory.Calculations
{
    public class OldFilmsCalculation: PriceCalculation
    {
        public OldFilmsCalculation(int rentDaysCount): base(rentDaysCount)
        {
        }

        public override int Perform()
        {
            int price = 0;
            if (rentDaysCount <= 5)
            {
                price = Consts.REGULAR_FEE;
            }
            else
            {
                var extraDays = rentDaysCount - 5;
                price = Consts.REGULAR_FEE + Consts.REGULAR_FEE * extraDays;
            }

            return price;
        }
    }
}
