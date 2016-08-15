namespace IntergalacticTravel.Tests
{
    using System.Collections.Generic;

    using IntergalacticTravel.Contracts;

    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class BusinessOwnerTests
    {
        //CollectProfits should increase the owner Resources by the total 
        //amount of Resources generated from the Teleport Stations that are in his possession.
        [Test]
        public void CollectProfits_ShouldIcreaseResourcesByTheReturnedAmount()
        {
            // Arrange
            var id = 1;
            var nickName = "nikcName";
            var teleportStations = new List<ITeleportStation>();

            var mockStationAlphaProfit = new Mock<IResources>();
            mockStationAlphaProfit.SetupGet(mock => mock.GoldCoins).Returns(10);
            mockStationAlphaProfit.SetupGet(mock => mock.SilverCoins).Returns(10);
            mockStationAlphaProfit.SetupGet(mock => mock.BronzeCoins).Returns(10);

            var mockStationBetaProfit = new Mock<IResources>();
            mockStationBetaProfit.SetupGet(mock => mock.GoldCoins).Returns(20);
            mockStationBetaProfit.SetupGet(mock => mock.SilverCoins).Returns(20);
            mockStationBetaProfit.SetupGet(mock => mock.BronzeCoins).Returns(20);

            // Arrange two TeleportStation
            var mockStationAlpha = new Mock<ITeleportStation>();
            mockStationAlpha.Setup(mock => mock.PayProfits(It.IsAny<IBusinessOwner>())).Returns(mockStationAlphaProfit.Object);
            var mockStationBeta = new Mock<ITeleportStation>();
            mockStationBeta.Setup(mock => mock.PayProfits(It.IsAny<IBusinessOwner>())).Returns(mockStationBetaProfit.Object);

            teleportStations.Add(mockStationAlpha.Object);
            teleportStations.Add(mockStationBeta.Object);

            var owner = new BusinessOwner(id, nickName, teleportStations);

            // Act.
            owner.CollectProfits();
            var actualResources = owner.Resources;

            // Assert.
            Assert.That(actualResources,
                Is.InstanceOf<IResources>()
                .With.Property("GoldCoins").EqualTo((uint)30)
                .And.Property("SilverCoins").EqualTo((uint)30)
                .And.Property("BronzeCoins").EqualTo((uint)30));
        }
    }
}
