namespace FastAndFurious.ConsoleApplication.Models.Tunnings.EngineControlUnits.Abstract
{
    using FastAndFurious.ConsoleApplication.Common.Enums;
    using FastAndFurious.ConsoleApplication.Contracts;
    using Tunnings.Abstract;

    public abstract class EngineControlUnit : AbstractTuningPart, IEngineControlUnit, ITunningPart, IAccelerateable, ITopSpeed, IWeightable, IValuable
    {
        private EngineControlUnitType engineControlUnitType;

        public EngineControlUnit(
            decimal price,
            int weight,
            int topSpeed,
            int acceleration,
            TunningGradeType gradeType,
            EngineControlUnitType engineControlUnitType)
            : base(price, weight, acceleration, topSpeed)
        {
            this.GradeType = gradeType;
            this.EngineControlUnitType = engineControlUnitType;
        }

        public EngineControlUnitType EngineControlUnitType
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
