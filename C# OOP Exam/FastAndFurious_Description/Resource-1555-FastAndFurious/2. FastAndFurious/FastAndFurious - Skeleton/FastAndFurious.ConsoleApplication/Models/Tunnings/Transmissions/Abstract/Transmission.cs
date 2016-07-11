
namespace FastAndFurious.ConsoleApplication.Models.Tunnings.Turbochargers.Abstract
{
    using System;
    using FastAndFurious.ConsoleApplication.Common.Enums;
    using FastAndFurious.ConsoleApplication.Contracts;
    using FastAndFurious.ConsoleApplication.Models.Tunnings.Abstract;

    public abstract class Transmission : AbstractTuningPart, ITransmission, ITunningPart
    {
        public Transmission(
           decimal price,
           int weight,
           int topSpeed,
           int acceleration,
           TunningGradeType gradeType,
           TransmissionType TransmissionType)
            : base(price, weight, acceleration, topSpeed)
        {
        }


        public TunningGradeType GradeType
        {
            get;
            protected set;
        }
        
        public TransmissionType TransmissionType
        {
            get;
            protected set;
        }
    }
}
