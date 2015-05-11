using System;
using System.Collections.Generic;
using System.Linq;
using FilmsInventory.Entities;
using FilmsInventory.Exceptions;
using FilmsInventory.Factories;
using FilmsInventory.Repositories;
using FilmsInventory.Utils;

namespace FilmsInventory.Services
{
    public class RentService: IRentService
    {
        protected IFilmRepository filmRepository;
        protected IRentRepository rentRepository;
        protected ICustomerRepository customerRepository;
        protected IEntityFactory entityFactory;
        protected IPriceCalculationFactory priceCalculationFactory;
        protected TimeProvider timeProvider;

        public RentService(IEntityFactory entityFactory,
                           IPriceCalculationFactory priceCalculationFactory,
                           IRentRepository rentRepository,
                           IFilmRepository filmRepository,
                           ICustomerRepository customerRepository,
                           TimeProvider timeProvider)
        {
            this.filmRepository = filmRepository;
            this.rentRepository = rentRepository;
            this.customerRepository = customerRepository;
            this.entityFactory = entityFactory;
            this.timeProvider = timeProvider;
            this.priceCalculationFactory = priceCalculationFactory;
        }

        public Rent RentFilmByCustomer(string filmName, string customerName, int daysCount)
        {
            if (String.IsNullOrEmpty(filmName) || String.IsNullOrEmpty(customerName))
            {
                throw new RequiredFieldNotSpecified();
            }

            if (daysCount <= 0)
            {
                throw new WrongDaysCountException();
            }

            var film = this.filmRepository.Load(filmName);

            if (film == null)
            {
                throw new FilmWithSpecifiedNameDoesNotExistException();
            }

            var now = this.timeProvider.UtcNow;
            var today = new DateTime(now.Year, now.Month, now.Day);
            var rent = this.entityFactory.CreateRent(filmName, customerName, today, daysCount);

            this.rentRepository.Save(rent);

            return rent;
        }

        public List<Rent> GetCustomersActiveRents(string customerName)
        {
            if (String.IsNullOrEmpty(customerName))
            {
                throw new RequiredFieldNotSpecified();
            }

            var allCustomersRents = this.rentRepository.LoadByCustomerName(customerName);

            var now = timeProvider.UtcNow;
            var today = new DateTime(now.Year, now.Month, now.Day);
            var activeRents = allCustomersRents.Where(r => r.IsActive(today));

            return activeRents.ToList();
        }

        public bool FilmIsNotAvailable(string filmName)
        {
            var rents = this.rentRepository.LoadAll();

            var now = this.timeProvider.UtcNow;
            var today = new DateTime(now.Year, now.Month, now.Day);

            var filmRentIsActive = rents.Any(r => r.FilmName == filmName && r.IsActive(today));

            return filmRentIsActive;
        }

        public List<Film> GetAvailableFilms()
        {
            var films = this.filmRepository.LoadAll();
            
            var rents = this.rentRepository.LoadAll();

            var now = timeProvider.UtcNow;
            var today = new DateTime(now.Year, now.Month, now.Day);

            var availableFilms = films.Where(f => !rents.Any(r => r.FilmName == f.Name && r.IsActive(today)));

            return availableFilms.ToList();
        }

        public int CalculatePrice(FilmType filmType, int rentDaysCount)
        {
            if (rentDaysCount <= 0)
            {
                throw new WrongDaysCountException();
            }

            var calculation = this.priceCalculationFactory.CreateCalculation(filmType, rentDaysCount);
            var price = calculation.Perform();

            return price;
        }

        public int CalculateTotalPriceOfCustomersActiveRents(string customerName)
        {
            var activeRents = GetCustomersActiveRents(customerName);
            int sum = 0;

            foreach (var rent in activeRents)
            {
                var film = this.filmRepository.Load(rent.FilmName);
                if (rent.Payment.Currency == Currency.EUR)
                {
                    sum += CalculatePrice(film.Type, rent.DaysCount);   
                }
            }

            return sum;
        }

        public Payment CreateEURRentPayment(FilmType filmType, int rentDaysCount)
        {
            if (rentDaysCount <= 0)
            {
                throw new WrongDaysCountException();
            }

            var price = CalculatePrice(filmType, rentDaysCount);

            var payment = this.entityFactory.CreatePayment(price, Currency.EUR);

            return payment;
        }

        public Rent AssignEURRentPayment(int rentId, Payment payment)
        {
            var rent = this.rentRepository.Load(rentId);

            if (rent == null)
            {
                throw new NotExistingRentException();
            }

            rent.AssignPayment(payment);

            this.rentRepository.Save(rent);

            return rent;
        }

        public Payment CreateBonusesRentPayment(FilmType filmType, int rentDaysCount)
        {
            if (rentDaysCount <= 0)
            {
                throw new WrongDaysCountException();
            }
            if (filmType != FilmType.NewReleases)
            {
                throw new CannotPayWithBonusesForNonNewRelaeseFilmsException();
            }

            var price = Consts.BONUSPOINTS_PRICE * rentDaysCount;

            var payment = this.entityFactory.CreatePayment(price, Currency.BonusPoints);

            return payment;
        }

        public Rent AssignBonusRentPayment(string customerName, int rentId, Payment payment)
        {
            var rent = this.rentRepository.Load(rentId);

            if (rent == null)
            {
                throw new NotExistingRentException();
            }

            var customer = this.customerRepository.Load(customerName);

            rent.AssignPayment(payment);

            customer.SpendBonusPoints(payment.Cost);

            this.rentRepository.Save(rent);
            this.customerRepository.Save(customer);

            return rent;
        }
    }
}
