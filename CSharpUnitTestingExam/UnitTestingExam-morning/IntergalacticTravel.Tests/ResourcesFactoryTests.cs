namespace IntergalacticTravel.Tests
{
    using System;
    using System.Collections.Generic;

    using IntergalacticTravel;

    using NUnit.Framework;

    [TestFixture]
    public class ResourcesFactoryTests
    {
        //GetResources should return a newly created Resources object
        //with correctly set up properties(Gold, Bronze and Silver coins), 
        //no matter what the order of the parameters is, when the input string is in 
        //the correct format. (Check with all possible cases):
        [TestCase("create resources gold(20) silver(30) bronze(40)")]
        [TestCase("create resources gold(20) bronze(40) silver(30)")]
        [TestCase("create resources silver(30) bronze(40) gold(20)")]
        [TestCase("create resources silver(30) gold(20) bronze(40)")]
        [TestCase("create resources bronze(40) gold(20) silver(30)")]
        [TestCase("create resources bronze(40) silver(30) gold(20)")]

        public void GetResources_ShouldReturnNewRousourcesObjectWithCorrectlySetProperties(string command)
        {
            var commandParams = command.Split(' ');
            var resourcesParameters = new Dictionary<char, uint>();
            for (int i = 2; i < commandParams.Length; i++)
            {
                var resourceType = commandParams[i];
                var key = resourceType[0];
                var paramz = resourceType.Split(
                    new char[] { '(', ')' },
                    StringSplitOptions.RemoveEmptyEntries);

                var value = uint.Parse(paramz[1]);

                resourcesParameters[key] = value;
            }
            var resourcesFactory = new ResourcesFactory();

            var actualResources = resourcesFactory.GetResources(command);

            Assert.That(actualResources,
                Is.InstanceOf<Resources>()
                .With.Property("GoldCoins").EqualTo(resourcesParameters['g'])
                .And.Property("SilverCoins").EqualTo(resourcesParameters['s'])
                .And.Property("BronzeCoins").EqualTo(resourcesParameters['b']));
        }
        //Example: The following lines should all create a new Resources object with 40 Bronze Coins, 30 Silver Coins and 20 Gold Coins.
        //create resources gold(20) silver(30) bronze(40)
        //create resources gold(20) bronze(40) silver(30)
        //create resources silver(30) bronze(40) gold(20)
        //create resources silver(30) gold(20) bronze(40)
        //create resources bronze(40) gold(20) silver(30)
        //create resources bronze(40) silver(30) gold(20)

        //GetResources should throw InvalidOperationException, 
        //which contains the string "command", when the input string represents an
        //invalid command. (Check with at least 2 different cases).
        [TestCase("create resources x y z")]
        [TestCase("tansta resources a b")]
        [TestCase("absolutelyRandomStringThatMustNotBeAValidCommand")]
        public void GetResources_ShouldThrowInvalidOperationException_WhenStringCommandParameterIsNotValid(string command)
        {
            var resourcesFactory = new ResourcesFactory();

            Assert.That(() => resourcesFactory.GetResources(command),
                Throws.InstanceOf<InvalidOperationException>().With.Message.Contains("Invalid command"));
        }
        //Invalid commands are any commands that does not follow the pattern described above.
        //Example:
        //create resources x y z
        //tansta resources a b
        //absolutelyRandomStringThatMustNotBeAValidCommand

        //GetResources should throw OverflowException, 
        //when the input string command is in the correct format,
        //but any of the values that represent the resource amount is larger
        //than uint.MaxValue. (Check with at least 2 different cases)
        [TestCase("create resources silver(10) gold(97853252356623523532) bronze(20)")]
        [TestCase("create resources silver(555555555555555555555555555555555) gold(97853252356623523532999999999) bronze(20)")]
        [TestCase("create resources silver(10) gold(20) bronze(4444444444444444444444444444444444444)")]
        public void GetResources_ShouldThrowOverflowException_WhenCommandIsCorrectButOneOfTheValuesIsLargerThanUIntMaxValue(string command)
        {
            var resourcesFactory = new ResourcesFactory();

            Assert.That(() => resourcesFactory.GetResources(command),
                Throws.InstanceOf<OverflowException>());
        }
        //Example:
        //create resources silver(10) gold(97853252356623523532) bronze(20)
        //create resources silver(555555555555555555555555555555555) gold(97853252356623523532999999999) bronze(20)
        //create resources silver(10) gold(20) bronze(4444444444444444444444444444444444444)
    }
}
