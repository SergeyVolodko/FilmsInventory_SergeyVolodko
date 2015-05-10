using FilmsInventory.Utils;

namespace FilmsInventory.Entities
{
    public class Customer
    {
        private readonly string name;
        private int bonusPoints;

        public string Name 
        {
            get { return this.name; }
        }
        public int BonusPoints
        {
            get { return this.bonusPoints; }
        }
        
        public Customer(string name)
        {
            this.name = name;
            this.bonusPoints = 0;
        }

        public void AddBonusPoints(int points)
        {
            this.bonusPoints += points;
        }

        public void SpendBonusPoints(int points)
        {
            this.bonusPoints -= points;
        }

        public bool HasEnoughtBonusPoints(int rentDaysCount)
        {
            return this.bonusPoints >= Consts.BONUSPOINTS_PRICE * rentDaysCount;
        }
    }
}
