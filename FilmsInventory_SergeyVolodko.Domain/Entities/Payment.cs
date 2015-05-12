
namespace FilmsInventory.Entities
{
    public enum Currency
    {
        EUR,
        BonusPoints
    }

    public class Payment
    {
        public int Cost { get; private set; }
        public Currency Currency { get; private set; }

        public Payment(int cost, Currency currency)
        {
            this.Cost = cost;
            this.Currency = currency;
        }
    }
}
