using Ninject;
using Ninject.Modules;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using ExamPrep.Data.Common.Services.Contracts;
using DbExam.Data.Common.Services;
using System.Data.Entity;
using DbExam.Data;
using ExamPrep.Data.Common.UnitsOfWork.Contracts;
using ExamPrep.Data.Common.UnitsOfWork;
using ExamPrep.Data.Common.Factories;
using ExamPrep.Data.Common.Repositories.Contracts;
using ExamPrep.Data.Common.Repositories;
using DbExam.Data.Common.Factories;
using DbExam.Models;

namespace DbExam.Data.JsonImporter.NinjectModules
{
    public class SuperheroesModule : NinjectModule
    {
        public override void Load()
        {
            this.Kernel.Bind(ctx =>
                 ctx
                 .FromAssembliesInPath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                 .SelectAllClasses()
                 .BindDefaultInterface());

            this.Bind<Func<Superhero>>().ToMethod(ctx => () => new Superhero());
            this.Bind<Func<Planet>>().ToMethod(ctx => () => new Planet());
            this.Bind<Func<Country>>().ToMethod(ctx => () => new Country());
            this.Bind<Func<City>>().ToMethod(ctx => () => new City());
            this.Bind<Func<Power>>().ToMethod(ctx => () => new Power());
            this.Bind<Func<Fraction>>().ToMethod(ctx => () => new Fraction());
            
            this.Bind<IUnitOfWork>().To<EfUnitOfWork>().Named("EfUnitOfWork");
            this.Bind<IUnitOfWorkFactory>().ToFactory().InSingletonScope();

            this.Bind<ISuperheroFactory>().ToFactory().InSingletonScope();

            this.Bind(typeof(IRepository<>)).To(typeof(EfGenericRepository<>));

            this.Bind<DbContext>().To<SuperheroesDbContext>().InSingletonScope();
            this.Bind(typeof(IService<>)).To(typeof(GenericService<>));
        }
    }
}
