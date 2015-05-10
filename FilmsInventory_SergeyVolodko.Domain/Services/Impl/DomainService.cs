using System;
using System.Collections.Generic;
using System.Linq;
using FilmsInventory.Entities;
using FilmsInventory.Exceptions;
using FilmsInventory.Factories;
using FilmsInventory.Repositories;

namespace FilmsInventory.Services
{
    public class DomainService: IDomainService
    {
        protected IEntityFactory entityFactory;
        protected IFilmRepository filmRepository;
        protected ICustomerRepository customerRepository;

        public DomainService(IEntityFactory entityFactory, IFilmRepository filmRepository, ICustomerRepository customerRepository)
        {
            this.entityFactory = entityFactory;
            this.filmRepository = filmRepository;
            this.customerRepository = customerRepository;
        }

        public Film CreateFilm(string name, FilmType type)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new RequiredFieldNotSpecified();
            }

            var film = this.entityFactory.CreateFilm(name, type);
            this.filmRepository.Save(film);

            return film;
        }

        public Film GetFilmByName(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new RequiredFieldNotSpecified();
            }

            return this.filmRepository.Load(name);
        }

        public List<Film> GetAllFilms()
        {
            return this.filmRepository.LoadAll().ToList();
        }

        public void RemoveFilm(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new RequiredFieldNotSpecified();
            }

            this.filmRepository.Remove(name);
        }

        public Film ChangeFilmType(string name, FilmType type)
        {
            var film = GetFilmByName(name);
            if (film == null)
            {
                throw new FilmWithSpecifiedNameDoesNotExistException();
            }
            
            film.ChangeType(type);

            this.filmRepository.Save(film);

            return film;
        }

        public Customer CreateCustomer(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new RequiredFieldNotSpecified();
            }

            var customer = this.entityFactory.CreateCustomer(name);

            this.customerRepository.Save(customer);

            return customer;
        }

        public Customer GetCustomer(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new RequiredFieldNotSpecified();
            }

            return this.customerRepository.Load(name);
        }

        public Customer GainBonusPoints(string customerName, FilmType filmType)
        {
            var customer = this.customerRepository.Load(customerName);

            if (filmType == FilmType.NewReleases)
            {
                customer.AddBonusPoints(2);
            }
            else
            {
                customer.AddBonusPoints(1);
            }

            this.customerRepository.Save(customer);

            return customer;
        }
        
        public bool CanCustomerPayByBonuses(string customerName, FilmType filmType, int rentDaysCount)
        {
            if (filmType != FilmType.NewReleases)
            {
                return false;
            }

            if (rentDaysCount <= 0)
            {
                throw new WrongDaysCountException();
            }

            var customer = this.customerRepository.Load(customerName);

            return customer.HasEnoughtBonusPoints(rentDaysCount);
        }
    }
}
