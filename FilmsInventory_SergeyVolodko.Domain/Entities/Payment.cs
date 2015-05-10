
namespace FilmsInventory.Entities
{
    public enum Currency
    {
        EUR,
        BonusPoints
    }

    public struct Payment
    {
        private readonly int cost;
        private readonly Currency currency;

        public int Cost
        {
            get { return this.cost; }
        }

        public Currency Currency
        {
            get { return this.currency; }
        }
        public Payment(int cost, Currency currency)
        {
            this.cost = cost;
            this.currency = currency;
        }
    }
}
