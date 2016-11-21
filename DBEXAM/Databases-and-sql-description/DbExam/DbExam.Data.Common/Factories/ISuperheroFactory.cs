using DbExam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbExam.Data.Common.Factories
{
    public interface ISuperheroFactory
    {
        Superhero CreateSuperhero();
    }
}
