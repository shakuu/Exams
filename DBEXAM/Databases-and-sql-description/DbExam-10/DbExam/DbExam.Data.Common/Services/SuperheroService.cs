using System;
using System.Collections.Generic;

using DbExam.Models;

using ExamPrep.Data.Common.Factories;
using ExamPrep.Data.Common.Repositories.Contracts;
using ExamPrep.Data.Common.Services.Contracts;
using System.Linq;

namespace DbExam.Data.Common.Services
{
    public class SuperheroService : GenericService<Superhero>, IService<Superhero>
    {
        private readonly IService<Planet> planetsService;
        private readonly IService<Country> countriesService;
        private readonly IService<City> citiesService;
        private readonly IService<Power> powersService;
        private readonly IService<Fraction> fractionService;

        public SuperheroService(
            IRepository<Superhero> repository,
            IUnitOfWorkFactory uowFactory,
            Func<Superhero> modelFactory,
            IService<Planet> planetsService,
            IService<Country> countriesService,
            IService<City> citiesService,
            IService<Power> powersService,
            IService<Fraction> fractionService)
            : base(repository, uowFactory, modelFactory)
        {
            this.planetsService = planetsService;
            this.countriesService = countriesService;
            this.citiesService = citiesService;
            this.powersService = powersService;
            this.fractionService = fractionService;
        }

        public void AddMany(IEnumerable<Superhero> superheroes)
        {
            using (var uow = base.uowFactory.GetEfUnitOfWork())
            {
                foreach (var superhero in superheroes)
                {
                    var hero = base.FindOrCreate(superhero);
                    if (hero.Id != 0)
                    {
                        continue;
                    }

                    hero.City.Country.Planet = this.planetsService.FindOrCreate(hero.City.Country.Planet);
                    hero.City.Country = this.countriesService.FindOrCreate(hero.City.Country);
                    hero.City = this.citiesService.FindOrCreate(hero.City);

                    if (hero.Powers != null)
                    {
                        var powers = new List<Power>();
                        foreach (var power in hero.Powers)
                        {
                            powers.Add(this.powersService.FindOrCreate(power));
                        }

                        hero.Powers = powers;
                    }

                    if (hero.Fractions != null)
                    {
                        var fractions = new List<Fraction>();
                        foreach (var fraction in hero.Fractions)
                        {
                            fractions.Add(this.fractionService.FindOrCreate(fraction));
                        }

                        hero.Fractions = fractions;
                    }

                    this.repository.Add(hero);
                }

                uow.Commit();
            }
        }

        public void ApplyPlanets()
        {
            var superheroes = this.repository.All();

            foreach (var superhero in superheroes)
            {
                using (var uow = this.uowFactory.GetEfUnitOfWork())
                {
                    if (superhero.Fractions != null)
                    {
                        foreach (var fraction in superhero.Fractions)
                        {
                            if (!fraction.Planets.Any(p => p.Name == superhero.City.Country.Planet.Name))
                            {
                                superhero.City.Country.Planet.Fractions.Add(fraction);
                            }
                        }
                    }

                    uow.Commit();
                }
            }
        }
    }
}
