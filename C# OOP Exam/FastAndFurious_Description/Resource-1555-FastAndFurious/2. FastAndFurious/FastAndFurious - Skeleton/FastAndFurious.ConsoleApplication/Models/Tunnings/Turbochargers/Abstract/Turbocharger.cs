using System;
using FastAndFurious.ConsoleApplication.Common.Enums;
using FastAndFurious.ConsoleApplication.Contracts;
using FastAndFurious.ConsoleApplication.Models.Tunnings.Abstract;

namespace FastAndFurious.ConsoleApplication.Models.Tunnings.Turbochargers.Abstract
{
    public abstract class Turbocharger : AbstractTuningPart, ITurbocharger, ITunningPart
    {
        public Turbocharger(
           decimal price,
           int weight,
           int topSpeed,
           int acceleration,
           TunningGradeType gradeType,
           TurbochargerType turbochargerType)
            : base(price, weight, acceleration, topSpeed)
        {
        }


        public TunningGradeType GradeType
        {
            get;
            protected set;
        }
        
        public TurbochargerType TurbochargerType
        {
            get;
            protected set;
        }
    }
}
