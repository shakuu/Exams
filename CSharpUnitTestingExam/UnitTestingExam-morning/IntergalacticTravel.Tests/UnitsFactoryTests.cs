namespace IntergalacticTravel.Tests
{
    using IntergalacticTravel;
    using IntergalacticTravel.Exceptions;

    using NUnit.Framework;

    [TestFixture]
    public class UnitsFactoryTests
    {
        //GetUnit should return new Procyon unit,
        //when a valid corresponding command is passed(i.e. "create unit Procyon Gosho 1");
        [Test]
        public void GetUnit_ShourReturnANewProcyonUnit_WhenAValidMatchingCommandParameterIsPassed()
        {
            var command = "create unit Procyon Gosho 1";
            var unitsFactory = new UnitsFactory();

            var actualUnit = unitsFactory.GetUnit(command);

            Assert.That(actualUnit, Is.Not.Null.And.InstanceOf<Procyon>());
        }

        //GetUnit should return new Luyten unit, 
        //when a valid corresponding command is passed(i.e. "create unit Luyten Pesho 2");
        [Test]
        public void GetUnit_ShouldReturnANewLuytenUnit_WhenAValidMatchingCommandParatemerIsPassed()
        {
            var command = "create unit Luyten Gosho 1";
            var unitsFactory = new UnitsFactory();

            var actualUnit = unitsFactory.GetUnit(command);

            Assert.That(actualUnit, Is.Not.Null.And.InstanceOf<Luyten>());
        }

        //GetUnit should return new Lacaille unit, 
        //hen a valid corresponding command is passed(i.e. "create unit Lacaille Tosho 3");
        [Test]
        public void GetUnit_ShouldReturnANewLacailleUnit_WhenAValidMatchingCommandParatemerIsPassed()
        {
            var command = "create unit Lacaille Gosho 1";
            var unitsFactory = new UnitsFactory();

            var actualUnit = unitsFactory.GetUnit(command);

            Assert.That(actualUnit, Is.Not.Null.And.InstanceOf<Lacaille>());
        }

        //GetUnit should throw InvalidUnitCreationCommandException,
        //when the command passed is not in the valid format described above. 
        //(Check for at least 2 different cases)
        [TestCase("")]
        [TestCase(null)]
        [TestCase("one two")]
        [TestCase("one two three")]
        [TestCase("one two three four")]
        [TestCase("one two three four five")]
        [TestCase("create unit Lacaille Tosho three")]
        
        public void GetUnit_ShouldThrowInvalidUnitCreationCommandException_WhenCommandParameterIsInvalid(string command)
        {
            var unitsFactory = new UnitsFactory();

            Assert.That(() => unitsFactory.GetUnit(command),
                Throws.InstanceOf<InvalidUnitCreationCommandException>());
        }
    }
}
