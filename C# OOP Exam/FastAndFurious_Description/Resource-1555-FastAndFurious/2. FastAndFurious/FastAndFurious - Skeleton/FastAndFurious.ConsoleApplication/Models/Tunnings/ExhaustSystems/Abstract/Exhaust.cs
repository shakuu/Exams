using System;
using FastAndFurious.ConsoleApplication.Common.Enums;
using FastAndFurious.ConsoleApplication.Contracts;
using FastAndFurious.ConsoleApplication.Models.Tunnings.Abstract;

namespace FastAndFurious.ConsoleApplication.Models.Tunnings.ExhaustSystems.Abstract
{
    public abstract class Exhaust : AbstractTuningPart, IExhaust, ITunningPart
    {
        private readonly ExhaustType exhaustType;

        public Exhaust(
           decimal price,
           int weight,
           int topSpeed,
           int acceleration,
           TunningGradeType gradeType,
           ExhaustType exhaustType)
            : base(price, weight, acceleration, topSpeed)
        {
            this.exhaustType = exhaustType;
            this.GradeType = gradeType;
        }

        public ExhaustType ExhaustType
        {
            get;
            protected set;
        }

        public TunningGradeType GradeType
        {
            get;
            protected set;
        }
        
    }
}
