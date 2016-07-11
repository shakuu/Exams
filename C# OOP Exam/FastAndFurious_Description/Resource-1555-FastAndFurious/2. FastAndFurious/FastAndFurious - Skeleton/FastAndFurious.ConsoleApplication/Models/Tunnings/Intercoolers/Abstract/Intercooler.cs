
namespace FastAndFurious.ConsoleApplication.Models.Tunnings.Turbochargers.Abstract
{
    using System;
    using FastAndFurious.ConsoleApplication.Common.Enums;
    using FastAndFurious.ConsoleApplication.Contracts;
    using FastAndFurious.ConsoleApplication.Models.Tunnings.Abstract;

    public abstract class Intercooler : AbstractTuningPart, IIntercooler, ITunningPart
    {
        public Intercooler(
           decimal price,
           int weight,
           int topSpeed,
           int acceleration,
           TunningGradeType gradeType,
           IntercoolerType IntercoolerType)
            : base(price, weight, acceleration, topSpeed)
        {
        }


        public TunningGradeType GradeType
        {
            get;
            protected set;
        }
        
        public IntercoolerType IntercoolerType
        {
            get;
            protected set;
        }
    }
}
