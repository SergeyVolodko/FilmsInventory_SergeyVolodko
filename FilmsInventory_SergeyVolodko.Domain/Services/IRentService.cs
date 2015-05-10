using System.Collections.Generic;
using FilmsInventory.Entities;

namespace FilmsInventory.Services
{
    public interface IRentService
    {
        Rent RentFilmByCustomer(string filmName, string customerName, int daysCount);
        List<Rent> GetCustomersActiveRents(string customerName);
        bool FilmIsNotAvailable(string filmName);
        List<Film> GetAvailableFilms();
        int CalculatePrice(FilmType filmType, int rentDaysCount);
        int CalculateTotalPriceOfCustomersActiveRents(string customerName);
        Payment CreateEURRentPayment(FilmType filmType, int rentDaysCount);
        Rent AssignEURRentPayment(int rentId, Payment payment);
        Payment CreateBonusesRentPayment(FilmType filmType, int rentDaysCount);
        Rent AssignBonusRentPayment(string customerName, int rentId, Payment payment);
    }
}
