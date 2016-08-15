namespace IntergalacticTravel.Tests
{
    using System;

    using IntergalacticTravel.Contracts;

    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class UnitTests
    {
        //Pay should throw NullReferenceException if the object passed is null.
        [Test]
        public void Pay_ShouldThrowNullReferenceException_WhenIResourcesParameterIsNull()
        {
            IResources cost = null;

            var id = 1;
            var nickName = "nickName";
            var unit = new Unit(id, nickName);

            Assert.That(() => unit.Pay(cost), Throws.InstanceOf<NullReferenceException>());
        }

        //Pay should decrease the owner's amount of Resources by the amount of the cost.
        [Test]
        public void Pay_ShouldDecreaseUnitResourcesByCost_WhenCostParamtersIsValid()
        {
            var cost = new Mock<IResources>();
            cost.SetupGet(mock => mock.GoldCoins).Returns(10);
            cost.SetupGet(mock => mock.SilverCoins).Returns(10);
            cost.SetupGet(mock => mock.BronzeCoins).Returns(10);

            var id = 1;
            var nickName = "nickName";
            var unit = new Unit(id, nickName);

            unit.Resources.GoldCoins = 11;
            unit.Resources.SilverCoins = 11;
            unit.Resources.BronzeCoins = 11;

            unit.Pay(cost.Object);
            var actualResources = unit.Resources;

            Assert.That(actualResources,
                Is.InstanceOf<IResources>()
                .With.Property("GoldCoins").EqualTo((uint)1)
                .And.Property("SilverCoins").EqualTo((uint)1)
                .And.Property("BronzeCoins").EqualTo((uint)1));
        }

        //Pay should return Resource object with the amount of resources
        //in the cost object.
        [Test]
        public void Pay_ShouldReturnIResourcesObjectWithTheAmountPaid()
        {
            var cost = new Mock<IResources>();
            cost.SetupGet(mock => mock.GoldCoins).Returns(10);
            cost.SetupGet(mock => mock.SilverCoins).Returns(10);
            cost.SetupGet(mock => mock.BronzeCoins).Returns(10);

            var id = 1;
            var nickName = "nickName";
            var unit = new Unit(id, nickName);

            unit.Resources.GoldCoins = 11;
            unit.Resources.SilverCoins = 11;
            unit.Resources.BronzeCoins = 11;

            var actualResources = unit.Pay(cost.Object);

            Assert.That(actualResources,
                Is.InstanceOf<IResources>()
                .With.Property("GoldCoins").EqualTo((uint)10)
                .And.Property("SilverCoins").EqualTo((uint)10)
                .And.Property("BronzeCoins").EqualTo((uint)10));
        }
    }
}
