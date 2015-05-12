using System;

namespace FilmsInventory.Entities
{
    public class Rent
    {
        public int Id { get; set; }
        public string CustomerName { get; private set; }
        public string FilmName { get; private set; }
        public int DaysCount { get; private set; }
        public DateTime StartDate { get; private set; }
        public Payment Payment { get; private set; }
        public DateTime EndDate
        {
            get { return this.StartDate.AddDays(DaysCount); }
        }

        public Rent(string customerName, string filmName, DateTime startDate, int daysCount)
        {
            this.CustomerName = customerName;
            this.FilmName = filmName;
            this.DaysCount = daysCount;
            this.StartDate = startDate;
        }

        public bool IsActive(DateTime date)
        {
            return this.StartDate <= date && date < this.EndDate;
        }

        public void AssignPayment(Payment payment)
        {
            this.Payment = payment;
        }
    }
}
