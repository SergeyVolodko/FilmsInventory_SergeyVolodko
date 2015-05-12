using FilmsInventory.Utils;

namespace FilmsInventory.Entities
{
    public class Customer
    {
        public string Name { get; private set; }
        public int BonusPoints { get; private set; }
        
        public Customer(string name)
        {
            this.Name = name;
            this.BonusPoints = 0;
        }

        public void AddBonusPoints(int points)
        {
            this.BonusPoints += points;
        }

        public void SpendBonusPoints(int points)
        {
            this.BonusPoints -= points;
        }

        public bool HasEnoughtBonusPoints(int rentDaysCount)
        {
            return this.BonusPoints >= Consts.BONUSPOINTS_PRICE * rentDaysCount;
        }
    }
}
