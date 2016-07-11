﻿using FastAndFurious.ConsoleApplication.Common.Enums;

namespace FastAndFurious.ConsoleApplication.Contracts
{
    public interface IExhaust : ITunningPart, IAccelerateable, ITopSpeed, IWeightable, IValuable 
    {
        ExhaustType ExhaustType { get; }
    }
}
