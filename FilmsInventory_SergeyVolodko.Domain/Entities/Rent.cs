using System;

namespace FilmsInventory.Entities
{
    public class Rent
    {
        private Payment payment;
        public int Id { get; set; }
        public string CustomerName { get; private set; }
        public string FilmName { get; private set; }
        public int DaysCount { get; private set; }
        public DateTime StartDate { get; private set; }

        public Payment Payment
        {
            get { return this.payment; }
        }
        public DateTime EndDate
        {
            get { return StartDate.AddDays(DaysCount); }
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
            return StartDate <= date && date < EndDate;
        }

        public void AssignPayment(Payment payment)
        {
            this.payment = payment;
        }
    }
}
