using System;
using System.Collections.Generic;
using FilmsInventory.Entities;
using FilmsInventory.Factories;
using FilmsInventory.Factories.Impl;
using FilmsInventory.Repositories;
using FilmsInventory.Services;
using FilmsInventory.Utils;
using Microsoft.Practices.Unity;

namespace FilmsInventory.ConsoleClient
{
    class Program
    {
        protected static IDomainService domainService;
        protected static IRentService rentService;

        private static List<Customer> customers;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Sergey's video salon!");
            Console.WriteLine("===>Creating customers and films...");
            Init();
            InitCustomers();
            InitFilms();
            ShowCustomers();
            ShowAllFilms();
            ShowAvailableFilms();

            Console.WriteLine();
            Console.WriteLine("===>Removing some films...");
            RemoveFewFilms();
            ShowAllFilms();

            Console.WriteLine();
            Console.WriteLine("===>Changing film type...");
            ChangeMatrixFilmType();

            Console.WriteLine();
            Console.WriteLine("===>Renting some films...");
            RentFewFilmsByDifferentCustomers();
            ShowAvailableFilms();
            
            Console.WriteLine();
            Console.WriteLine("===>Customers' active rents:");
            ShowCustomerRentals(customers[0].Name);
            ShowCustomerRentals(customers[1].Name);

            Console.WriteLine();
            Console.WriteLine("===>Renting many films by one customer...");
            RentManyFilmsByIgorNikolaev();
            Console.WriteLine();
            ShowCustomerRentals(customers[0].Name);

            Console.ReadLine();
        }
        
        private static void Init()
        {
            var container = new UnityContainer();
            Bootstrapper.Configure(container);

            domainService = container.Resolve<Services.DomainService>();
            rentService = container.Resolve<Services.RentService>();
        }

        private static void InitCustomers()
        {
            customers = new List<Customer>();

            var customer = domainService.CreateCustomer("Igor Nikolaev");
            customers.Add(customer);
            
            customer = domainService.CreateCustomer("Jhon Travolta");
            customers.Add(customer);
        }
        
        private static void InitFilms()
        {
            domainService.CreateFilm("Matrix", FilmType.NewReleases);
            domainService.CreateFilm("Matrix 11", FilmType.NewReleases);
            domainService.CreateFilm("Matrix 111", FilmType.NewReleases);
            domainService.CreateFilm("Lord of the rings", FilmType.NewReleases);
            domainService.CreateFilm("Lord of the rings 11", FilmType.NewReleases);
            domainService.CreateFilm("Lord of the rings 111", FilmType.NewReleases);
            domainService.CreateFilm("Gran Torino", FilmType.NewReleases);
            domainService.CreateFilm("Shattered island", FilmType.NewReleases);
            domainService.CreateFilm("The Great Gatsby", FilmType.NewReleases);
            domainService.CreateFilm("Inception", FilmType.NewReleases);
            domainService.CreateFilm("Zombieland", FilmType.NewReleases);
            domainService.CreateFilm("Ted", FilmType.NewReleases);
            domainService.CreateFilm("Hangover", FilmType.NewReleases);

            domainService.CreateFilm("Back to the future", FilmType.RegularFilms);
            domainService.CreateFilm("Back to the future 11", FilmType.RegularFilms);
            domainService.CreateFilm("Back to the future 111", FilmType.RegularFilms);
            domainService.CreateFilm("Home alone", FilmType.RegularFilms);

            domainService.CreateFilm("Titanic", FilmType.OldFilms);
            domainService.CreateFilm("The godfather", FilmType.OldFilms);
            domainService.CreateFilm("From dusk till down", FilmType.OldFilms);
        }

        private static void ShowCustomers()
        {
            Console.WriteLine("Customers:");
            for (int i = 0; i < customers.Count; i++)
            {
                Console.WriteLine(String.Format("{0}. {1}", i + 1, customers[i].Name));
            }
        }

        private static void ShowAllFilms()
        {
            var allFilms = domainService.GetAllFilms();
            Console.WriteLine();
            Console.WriteLine("===>All films:");
            ShowFilms(allFilms);
        }

        private static void ShowCustomerRentals(string customerName)
        {
            var rents = rentService.GetCustomersActiveRents(customerName);
            Console.WriteLine("{0}'s rents:", customerName);
            foreach (var rent in rents)
            {
                ShowRentedFilm(rent);
            }
            var total = rentService.CalculateTotalPriceOfCustomersActiveRents(customerName);
            Console.WriteLine("Total: {0} EUR", total);
        }

        private static void ShowAvailableFilms()
        {
            var availableFilms = rentService.GetAvailableFilms();
            Console.WriteLine();
            Console.WriteLine("===>Available films:");
            ShowFilms(availableFilms);
        }

            private static void ShowFilms(List<Film> films)
            {
                for (int i = 0; i < films.Count; i++)
                {
                    Console.Write(i + 1 + ". ");
                    ShowFilm(films[i]);
                }
            }

                private static void ShowFilm(Film film)
                {
                    Console.WriteLine(String.Format("{0} ({1})", film.Name, film.Type));
                }

        private static void ChangeMatrixFilmType()
        {
            var film = domainService.GetFilmByName("Matrix");
            Console.WriteLine(String.Format("{0} film type from {1} to {2}", film.Name, film.Type, FilmType.RegularFilms));
            film = domainService.ChangeFilmType(film.Name, FilmType.RegularFilms);
            ShowFilm(film);
        }
        
        private static void RemoveFewFilms()
        {
            Console.WriteLine("Removing two films: BACK TO THE FUTURE parts II and III");
            domainService.RemoveFilm("Back to the future 11");
            domainService.RemoveFilm("Back to the future 111");
        }

        private static void RentFewFilmsByDifferentCustomers()
        {
            ProcessFilmRent(customers[0].Name, "Matrix", 3);
            ProcessFilmRent(customers[0].Name, "Ted", 2);
            ProcessFilmRent(customers[1].Name, "From dusk till down", 7);
        }

        private static void RentManyFilmsByIgorNikolaev()
        {
            var igorName = customers[0].Name;
            ProcessFilmRent(igorName, "Matrix 11", 5);
            ProcessFilmRent(igorName, "Titanic", 10);
            ProcessFilmRent(igorName, "Matrix 111", 3);
            ProcessFilmRent(igorName, "Home alone", 2);
            ProcessFilmRent(igorName, "Lord of the rings", 1);
            ProcessFilmRent(igorName, "Lord of the rings 11", 2);
            ProcessFilmRent(igorName, "Lord of the rings 111", 3);
            ProcessFilmRent(igorName, "Gran Torino", 4);
            ProcessFilmRent(igorName, "Shattered island", 2);
            ProcessFilmRent(igorName, "The Great Gatsby", 6);
            ProcessFilmRent(igorName, "Inception", 4);
            ProcessFilmRent(igorName, "Zombieland", 3);
            ProcessFilmRent(igorName, "Hangover", 1);
        }
        
            private static void ProcessFilmRent(string customerName, string filmName, int rentDaysCount)
            {
                Console.WriteLine(String.Format(">Please enter film name and rent period in days: {0} {1}", filmName, rentDaysCount));
            
                if (rentService.FilmIsNotAvailable(filmName))
                {
                    Console.WriteLine(String.Format("Sorry, {0} is not available at the moment", filmName));
                    return; 
                }
                var film = domainService.GetFilmByName(filmName);
                var customer = domainService.GetCustomer(customerName);

                ConfirmPrice(customer, film, rentDaysCount);
                var payment = ProcessPayment(customerName, film.Type, rentDaysCount);
                var rent = rentService.RentFilmByCustomer(filmName, customerName, rentDaysCount);
            
                rent = AssignPayment(customerName, rent.Id, payment);

                ShowReceipt(rent);
            }

                private static void ConfirmPrice(Customer customer, Film film, int rentDaysCount)
                {
                    if (domainService.CanCustomerPayByBonuses(customer.Name, film.Type, rentDaysCount))
                    {
                        Console.WriteLine(String.Format(">You have enought bonus points to pay for this rent ({0} points) Would you like to pay by bonuses?: y", customer.BonusPoints));
                    }
                    else
                    {
                        var price = rentService.CalculatePrice(film.Type, rentDaysCount);
                        Console.WriteLine(String.Format(">Your rent price is {0} EUR. Do you confirm a payment?: y", price));
                    }
                }

                private static Payment ProcessPayment(string customerName, FilmType filmType, int rentDaysCount)
                {
                    if (domainService.CanCustomerPayByBonuses(customerName, filmType, rentDaysCount))
                    {
                        return rentService.CreateBonusesRentPayment(filmType, rentDaysCount);
                    }
                    else
                    {
                        domainService.GainBonusPoints(customerName, filmType);
                        return rentService.CreateEURRentPayment(filmType, rentDaysCount);
                    }
                }

                private static Rent AssignPayment(string customerName, int rentId, Payment payment)
                {
                    if (payment.Currency == Currency.BonusPoints)
                    {
                        return rentService.AssignBonusRentPayment(customerName, rentId, payment);
                    }
                    else
                    {
                        return rentService.AssignEURRentPayment(rentId, payment);
                    }
                }

                private static void ShowReceipt(Rent rent)
                {
                    ShowRentedFilm(rent);
                    var customer = domainService.GetCustomer(rent.CustomerName);
                    Console.WriteLine(String.Format("Customer: {0} bonus points: {1}", customer.Name, customer.BonusPoints));
                    Console.WriteLine("-------------------------------");
                }

                    private static void ShowRentedFilm(Rent rent)
                    {
                        var film = domainService.GetFilmByName(rent.FilmName);

                        if (rent.Payment.Currency == Currency.BonusPoints)
                        {
                            Console.WriteLine(String.Format("{0} ({1}) {2} days (payed with {3} bonus points)", film.Name, film.Type, rent.DaysCount, rent.Payment.Cost));
                        }
                        else
                        {
                            Console.WriteLine(String.Format("{0} ({1}) {2} days {3} EUR", film.Name, film.Type, rent.DaysCount, rent.Payment.Cost));
                        }
                    }
    }
}
