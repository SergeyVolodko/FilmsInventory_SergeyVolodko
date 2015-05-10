using FilmsInventory.Utils;

namespace FilmsInventory.Tests.Data
{
    public static class RentData
    {
        public static int TwoDays = 2;
        public static int FiveDays = 5;
        public static int SevenDays = 7;
        public static int NegativeDay = -3;

        public static int NewReleasesPriceForTwoDays = 2 * Consts.PREMIUM_FEE;
        public static int RegularFilmsPriceForTwoDays = Consts.REGULAR_FEE;
        public static int RegularFilmsPriceForFiveDays = Consts.REGULAR_FEE + 2 * Consts.REGULAR_FEE;
        public static int OldFilmsPriceForTwoDays = Consts.REGULAR_FEE;
        public static int OldFilmsPriceForSevenDays = Consts.REGULAR_FEE + 2 * Consts.REGULAR_FEE;

        public static int NewReleaseBonusPointsPriceForTwoDays = 2 * Consts.BONUSPOINTS_PRICE;
    }
}
