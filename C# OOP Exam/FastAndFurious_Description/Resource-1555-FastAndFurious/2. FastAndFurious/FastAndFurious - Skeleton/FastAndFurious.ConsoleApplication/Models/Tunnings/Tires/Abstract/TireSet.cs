using System;
using FastAndFurious.ConsoleApplication.Common.Enums;
using FastAndFurious.ConsoleApplication.Contracts;
using FastAndFurious.ConsoleApplication.Models.Tunnings.Abstract;

namespace FastAndFurious.ConsoleApplication.Models.Tunnings.Tires.Abstract
{
    public abstract class TiresSet : AbstractTuningPart, ITunningPart, ITireSet 
    {
        public TiresSet(
           decimal price,
           int weight,
           int topSpeed,
           int acceleration,
           TunningGradeType gradeType,
           TireType TireType)
            : base(price, weight, acceleration, topSpeed)
        {
        }
        
        public TunningGradeType GradeType
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        
        public TireType TireType
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
