namespace IntergalacticTravel.Tests
{
    using System.Collections.Generic;

    using IntergalacticTravel.Contracts;
    using IntergalacticTravel.Exceptions;
    using IntergalacticTravel.Tests.Mocks;

    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class TeleportStationTests
    {
        //Constructor should set up all of the provided fields(owner, galacticMap & location), 
        //when a new TeleportStation is created with valid parameters passed to the constructor.
        [Test]
        public void Constructor_ShouldSetUpAllFields_WhenPassedValidParameters()
        {
            // Arrange.
            var mockOwner = new Mock<IBusinessOwner>();
            var mockGalacticMap = new Mock<IEnumerable<IPath>>();
            var mockLocation = new Mock<ILocation>();

            // Act.
            var teleportStation = new ExposedProtectedFieldsMockTeleportStation(
                mockOwner.Object, mockGalacticMap.Object, mockLocation.Object);

            // Assert.
            Assert.That(teleportStation,
                Is.InstanceOf<TeleportStation>()
                .With.Property("Owner").SameAs(mockOwner.Object)
                .And.Property("Location").SameAs(mockLocation.Object)
                .And.Property("GalacticMap").SameAs(mockGalacticMap.Object)
                .And.Property("Resources").Not.Null);
        }

        //TeleportUnit should throw ArgumentNullException,
        //with a message that contains the string "unitToTeleport", when IUnit unitToTeleport is null.
        [Test]
        public void TeleportUnit_ShouldThrowArgumentNullException_WhenUnitToTeleportParameterIsNull()
        {
            // Arrange
            var mockOwner = new Mock<IBusinessOwner>();
            var mockGalacticMap = new Mock<IEnumerable<IPath>>();
            var mockLocation = new Mock<ILocation>();

            IUnit mockUnitToTeleport = null;
            var mockTargetLocation = new Mock<ILocation>();

            var teleportStation = new TeleportStation(
                 mockOwner.Object, mockGalacticMap.Object, mockLocation.Object);

            // Act and Assert.
            Assert.That(() => teleportStation.TeleportUnit(mockUnitToTeleport, mockTargetLocation.Object),
                Throws.ArgumentNullException.With.Message.Contains("unitToTeleport"));
        }

        //TeleportUnit should throw ArgumentNullException, 
        //with a message that contains the string "destination", when ILocation destination is null.
        [Test]
        public void TeleportUnit_ShouldThrowArgumentNullException_WhenILocationDestinationParamterIsNull()
        {
            // Arrange TeleportStation constructor parameters.
            var mockOwner = new Mock<IBusinessOwner>();
            var mockGalacticMap = new Mock<IEnumerable<IPath>>();
            var mockLocation = new Mock<ILocation>();

            var mockUnitToTeleport = new Mock<IUnit>();
            ILocation mockTargetLocation = null;

            var teleportStation = new TeleportStation(
                 mockOwner.Object, mockGalacticMap.Object, mockLocation.Object);

            // Act and Assert.
            Assert.That(() => teleportStation.TeleportUnit(mockUnitToTeleport.Object, mockTargetLocation),
                Throws.ArgumentNullException.With.Message.Contains("destination"));
        }

        //TeleportUnit should throw TeleportOutOfRangeException, 
        //with a message that contains the string "unitToTeleport.CurrentLocation", 
        //when а unit is trying to use the TeleportStation from a distant location (another planet for example).
        [Test]
        public void TeleportUnit_ShouldThrowTeleportOutOfRangeException_WhenUnitToTeleportLocationIsNotValid()
        {
            // Arrange unitToTeleport parameter.
            var mockUnitToTeleport = new Mock<IUnit>();
            var mockLocationUnit = new Mock<ILocation>();
            var mockPlanetUnit = new Mock<IPlanet>();
            var mockGalaxyUnit = new Mock<IGalaxy>();
            mockPlanetUnit.SetupGet(mock => mock.Name).Returns("planet1");
            mockPlanetUnit.SetupGet(mock => mock.Galaxy).Returns(mockGalaxyUnit.Object);
            mockGalaxyUnit.SetupGet(mock => mock.Name).Returns("galaxy1");
            mockLocationUnit.SetupGet(mock => mock.Planet).Returns(mockPlanetUnit.Object);
            mockUnitToTeleport.SetupGet(mock => mock.CurrentLocation).Returns(mockLocationUnit.Object);

            // Arrange TeleportStation Constructor parameters.
            var mockOwner = new Mock<IBusinessOwner>();
            var mockGalacticMap = new Mock<IEnumerable<IPath>>();

            var mockLocationStation = new Mock<ILocation>();
            var mockPlanetStation = new Mock<IPlanet>();
            var mockGalaxyStation = new Mock<IGalaxy>();
            mockPlanetStation.SetupGet(mock => mock.Name).Returns("planet2");
            mockPlanetStation.SetupGet(mock => mock.Galaxy).Returns(mockGalaxyStation.Object);
            mockGalaxyStation.SetupGet(mock => mock.Name).Returns("galaxy2");
            mockLocationStation.SetupGet(mock => mock.Planet).Returns(mockPlanetStation.Object);

            // Arrange targetLocation Parameter.
            var mockTargetLocation = new Mock<ILocation>();

            var teleportStation = new TeleportStation(
                 mockOwner.Object, mockGalacticMap.Object, mockLocationStation.Object);

            // Act and Assert.
            Assert.That(() => teleportStation.TeleportUnit(mockUnitToTeleport.Object, mockTargetLocation.Object),
                Throws.InstanceOf<TeleportOutOfRangeException>().With.Message.Contains("unitToTeleport.CurrentLocation"));
        }

        //TeleportUnit should throw InvalidTeleportationLocationException,
        //with a message that contains the string "units will overlap"
        //when trying to teleport a unit to a location which another unit has already taken.
        [Test]
        public void TeleportUnit_ShouldThrowInvalidTeleportationLocationException_WhenDestinationLocationParameterHasUnits()
        {
            // Arrange TeleportStation constructor parameters.
            var mockOwner = new Mock<IBusinessOwner>();
            var mockGalacticMap = new List<IPath>();
            var mockLocationStation = new Mock<ILocation>();
            var mockPlanetStation = new Mock<IPlanet>();
            var mockGalaxyStation = new Mock<IGalaxy>();
            mockPlanetStation.SetupGet(mock => mock.Name).Returns("planetStation");
            mockPlanetStation.SetupGet(mock => mock.Galaxy).Returns(mockGalaxyStation.Object);
            mockGalaxyStation.SetupGet(mock => mock.Name).Returns("galaxyStation");
            mockLocationStation.SetupGet(mock => mock.Planet).Returns(mockPlanetStation.Object);

            // Arrange Target Location.
            var mockGalaxy = new Mock<IGalaxy>();
            mockGalaxy.SetupGet(mock => mock.Name).Returns("galaxy");

            // Arrange the conflicting unit which will cause the exception.
            var mockUnitAtLocation = new Mock<IUnit>();
            mockUnitAtLocation.SetupGet(mock => mock.CurrentLocation.Coordinates.Latitude).Returns((double)0);
            mockUnitAtLocation.SetupGet(mock => mock.CurrentLocation.Coordinates.Longtitude).Returns((double)0);
            mockUnitAtLocation.SetupGet(mock => mock.CurrentLocation.Planet.Name).Returns("planet");
            mockUnitAtLocation.SetupGet(mock => mock.CurrentLocation.Planet.Galaxy.Name).Returns("galaxy");

            var mockPlanet = new Mock<IPlanet>();
            mockPlanet.SetupGet(mock => mock.Name).Returns("planet");
            mockPlanet.SetupGet(mock => mock.Galaxy).Returns(mockGalaxy.Object);
            mockPlanet.SetupGet(mock => mock.Units).Returns(new List<IUnit>() { mockUnitAtLocation.Object });

            var mockLocationPath = new Mock<ILocation>();
            mockLocationPath.SetupGet(mock => mock.Planet).Returns(mockPlanet.Object);

            var mockPathToDestination = new Mock<IPath>();
            mockPathToDestination.SetupGet(mock => mock.TargetLocation).Returns(mockLocationPath.Object);
            mockGalacticMap.Add(mockPathToDestination.Object);

            // Arrange unitToTeleport, currentLocation returns the same location as the TeleportStation itself.
            var mockUnitToTeleport = new Mock<IUnit>();
            mockUnitToTeleport.SetupGet(mock => mock.CurrentLocation).Returns(mockLocationStation.Object);

            var mockTargetLocation = new Mock<ILocation>();
            mockTargetLocation.SetupGet(mock => mock.Planet).Returns(mockPlanet.Object);
            mockTargetLocation.SetupGet(mock => mock.Coordinates.Longtitude).Returns((double)0);
            mockTargetLocation.SetupGet(mock => mock.Coordinates.Longtitude).Returns((double)0);

            // Arrange TeleportStation.
            var teleportStation = new TeleportStation(
                 mockOwner.Object, mockGalacticMap, mockLocationStation.Object);

            // Act and Assert.
            Assert.That(() => teleportStation.TeleportUnit(mockUnitToTeleport.Object, mockTargetLocation.Object),
                Throws.InstanceOf<InvalidTeleportationLocationException>().With.Message.Contains("units will overlap"));
        }

        //TeleportUnit should throw LocationNotFoundException, 
        //with a message that contains the string "Galaxy",
        //when trying to teleport a unit to a Galaxy,
        //which is not found in the locations list of the teleport station.
        [Test]
        public void TeleportUnit_ShouldThrowLocationNotFoundException_WhenPathToGalaxyIsNotFound()
        {
            // Arrange TeleportStation constructor.
            var mockOwner = new Mock<IBusinessOwner>();
            var mockGalacticMap = new List<IPath>();
            var mockLocationStation = new Mock<ILocation>();
            var mockPlanetStation = new Mock<IPlanet>();
            var mockGalaxyStation = new Mock<IGalaxy>();
            mockPlanetStation.SetupGet(mock => mock.Name).Returns("planetStation");
            mockPlanetStation.SetupGet(mock => mock.Galaxy).Returns(mockGalaxyStation.Object);
            mockGalaxyStation.SetupGet(mock => mock.Name).Returns("galaxyStation");
            mockLocationStation.SetupGet(mock => mock.Planet).Returns(mockPlanetStation.Object);

            // Arrange targetLocation.
            var mockGalaxy = new Mock<IGalaxy>();
            mockGalaxy.SetupGet(mock => mock.Name).Returns("galaxy");

            var mockUnitAtLocation = new Mock<IUnit>();
            mockUnitAtLocation.SetupGet(mock => mock.CurrentLocation.Coordinates.Latitude).Returns((double)0);
            mockUnitAtLocation.SetupGet(mock => mock.CurrentLocation.Coordinates.Longtitude).Returns((double)0);
            mockUnitAtLocation.SetupGet(mock => mock.CurrentLocation.Planet.Name).Returns("planet");
            mockUnitAtLocation.SetupGet(mock => mock.CurrentLocation.Planet.Galaxy.Name).Returns("galaxy");

            var mockPlanet = new Mock<IPlanet>();
            mockPlanet.SetupGet(mock => mock.Name).Returns("planet");
            mockPlanet.SetupGet(mock => mock.Galaxy).Returns(mockGalaxy.Object);
            mockPlanet.SetupGet(mock => mock.Units).Returns(new List<IUnit>() { mockUnitAtLocation.Object });

            // Arrange the path, returns a unique Galaxy.Name, causing the exception.
            var mockLocationPath = new Mock<ILocation>();
            mockLocationPath.SetupGet(mock => mock.Planet.Galaxy.Name).Returns("a galaxy far far away");

            var mockPathToDestination = new Mock<IPath>();
            mockPathToDestination.SetupGet(mock => mock.TargetLocation).Returns(mockLocationPath.Object);
            mockGalacticMap.Add(mockPathToDestination.Object);

            var mockUnitToTeleport = new Mock<IUnit>();
            mockUnitToTeleport.SetupGet(mock => mock.CurrentLocation).Returns(mockLocationStation.Object);

            var mockTargetLocation = new Mock<ILocation>();
            mockTargetLocation.SetupGet(mock => mock.Planet).Returns(mockPlanet.Object);
            mockTargetLocation.SetupGet(mock => mock.Coordinates.Longtitude).Returns((double)0);
            mockTargetLocation.SetupGet(mock => mock.Coordinates.Longtitude).Returns((double)0);

            var teleportStation = new TeleportStation(
                 mockOwner.Object, mockGalacticMap, mockLocationStation.Object);

            // Act and Assert.
            Assert.That(() => teleportStation.TeleportUnit(mockUnitToTeleport.Object, mockTargetLocation.Object),
                Throws.InstanceOf<LocationNotFoundException>().With.Message.Contains("Galaxy"));
        }

        //TeleportUnit should throw LocationNotFoundException, 
        //with a message that contains the string "Planet", 
        //when trying to teleport a unit to a Planet,
        //which is not found in the locations list of the teleport station.
        [Test]
        public void TeleportUnit_ShouldThrowLocationNotFoundException_WhenPathToPlanetIsNotFound()
        {
            // Arrange TeleportStation constructor parameters.
            var mockOwner = new Mock<IBusinessOwner>();
            var mockGalacticMap = new List<IPath>();
            var mockLocationStation = new Mock<ILocation>();
            var mockPlanetStation = new Mock<IPlanet>();
            var mockGalaxyStation = new Mock<IGalaxy>();
            mockPlanetStation.SetupGet(mock => mock.Name).Returns("planetStation");
            mockPlanetStation.SetupGet(mock => mock.Galaxy).Returns(mockGalaxyStation.Object);
            mockGalaxyStation.SetupGet(mock => mock.Name).Returns("galaxyStation");
            mockLocationStation.SetupGet(mock => mock.Planet).Returns(mockPlanetStation.Object);

            // Arrange targetLocation.
            var mockGalaxy = new Mock<IGalaxy>();
            mockGalaxy.SetupGet(mock => mock.Name).Returns("galaxy");

            var mockUnitAtLocation = new Mock<IUnit>();
            mockUnitAtLocation.SetupGet(mock => mock.CurrentLocation.Coordinates.Latitude).Returns((double)0);
            mockUnitAtLocation.SetupGet(mock => mock.CurrentLocation.Coordinates.Longtitude).Returns((double)0);
            mockUnitAtLocation.SetupGet(mock => mock.CurrentLocation.Planet.Name).Returns("planet");
            mockUnitAtLocation.SetupGet(mock => mock.CurrentLocation.Planet.Galaxy.Name).Returns("galaxy");

            var mockPlanet = new Mock<IPlanet>();
            mockPlanet.SetupGet(mock => mock.Name).Returns("planet");
            mockPlanet.SetupGet(mock => mock.Galaxy).Returns(mockGalaxy.Object);
            mockPlanet.SetupGet(mock => mock.Units).Returns(new List<IUnit>() { mockUnitAtLocation.Object });

            // Arrange the path, returns a unique Planet.Name, causing the exception.
            var mockLocationPath = new Mock<ILocation>();
            mockLocationPath.SetupGet(mock => mock.Planet.Galaxy.Name).Returns("galaxy");
            mockLocationPath.SetupGet(mock => mock.Planet.Name).Returns("*^&*^*^^&");

            var mockPathToDestination = new Mock<IPath>();
            mockPathToDestination.SetupGet(mock => mock.TargetLocation).Returns(mockLocationPath.Object);
            mockGalacticMap.Add(mockPathToDestination.Object);

            var mockUnitToTeleport = new Mock<IUnit>();
            mockUnitToTeleport.SetupGet(mock => mock.CurrentLocation).Returns(mockLocationStation.Object);

            var mockTargetLocation = new Mock<ILocation>();
            mockTargetLocation.SetupGet(mock => mock.Planet).Returns(mockPlanet.Object);
            mockTargetLocation.SetupGet(mock => mock.Coordinates.Longtitude).Returns((double)0);
            mockTargetLocation.SetupGet(mock => mock.Coordinates.Longtitude).Returns((double)0);

            var teleportStation = new TeleportStation(
                 mockOwner.Object, mockGalacticMap, mockLocationStation.Object);

            // Act and Assert.
            Assert.That(() => teleportStation.TeleportUnit(mockUnitToTeleport.Object, mockTargetLocation.Object),
                Throws.InstanceOf<LocationNotFoundException>().With.Message.Contains("Planet"));
        }

        //TeleportUnit should throw InsufficientResourcesException, 
        //with a message that contains the string "FREE LUNCH",
        //when trying to teleport a unit to a given Location, but the service costs more 
        //than the unit's current available resources.
        [Test]
        public void TeleportUnit_ShouldThrowInsufficientResourcesException_WhenServiceCostsMoreThanUnitsResources()
        {
            var mockOwner = new Mock<IBusinessOwner>();
            var mockGalacticMap = new List<IPath>();
            var mockLocationStation = new Mock<ILocation>();
            var mockPlanetStation = new Mock<IPlanet>();
            var mockGalaxyStation = new Mock<IGalaxy>();
            mockPlanetStation.SetupGet(mock => mock.Name).Returns("planetStation");
            mockPlanetStation.SetupGet(mock => mock.Galaxy).Returns(mockGalaxyStation.Object);
            mockGalaxyStation.SetupGet(mock => mock.Name).Returns("galaxyStation");
            mockLocationStation.SetupGet(mock => mock.Planet).Returns(mockPlanetStation.Object);

            var mockGalaxy = new Mock<IGalaxy>();
            mockGalaxy.SetupGet(mock => mock.Name).Returns("galaxy");

            var mockPlanet = new Mock<IPlanet>();
            mockPlanet.SetupGet(mock => mock.Name).Returns("planet");
            mockPlanet.SetupGet(mock => mock.Galaxy).Returns(mockGalaxy.Object);
            mockPlanet.SetupGet(mock => mock.Units).Returns(new List<IUnit>() { });

            var mockLocationPath = new Mock<ILocation>();
            mockLocationPath.SetupGet(mock => mock.Planet).Returns(mockPlanet.Object);

            var mockPathToDestination = new Mock<IPath>();
            mockPathToDestination.SetupGet(mock => mock.TargetLocation).Returns(mockLocationPath.Object);
            mockGalacticMap.Add(mockPathToDestination.Object);

            // Arrange mock unit to teleport. Setup CanPay to return False explicitly. ( moq-ed bool returns false by default anyway ). 
            var mockUnitToTeleport = new Mock<IUnit>();
            mockUnitToTeleport.SetupGet(mock => mock.CurrentLocation).Returns(mockLocationStation.Object);
            mockUnitToTeleport.Setup(mock => mock.CanPay(It.IsAny<IResources>())).Returns(false);

            var mockTargetLocation = new Mock<ILocation>();
            mockTargetLocation.SetupGet(mock => mock.Planet).Returns(mockPlanet.Object);
            mockTargetLocation.SetupGet(mock => mock.Coordinates.Longtitude).Returns((double)0);
            mockTargetLocation.SetupGet(mock => mock.Coordinates.Longtitude).Returns((double)0);

            var teleportStation = new TeleportStation(
                 mockOwner.Object, mockGalacticMap, mockLocationStation.Object);

            // Act and Assert.
            Assert.That(() => teleportStation.TeleportUnit(mockUnitToTeleport.Object, mockTargetLocation.Object),
                Throws.InstanceOf<InsufficientResourcesException>().With.Message.Contains("FREE LUNCH"));
        }

        //TeleportUnit should require a payment from the unitToTeleport for the provided services, 
        //when all of the validations pass successfully and the unit is ready for teleportation.
        [Test]
        public void TeleportUnit_ShouldAccessUnitToTeleportPay_WhenInputParametersAreValid()
        {
            // Arrange 
            var mockOwner = new Mock<IBusinessOwner>();
            var mockGalacticMap = new List<IPath>();
            var mockLocationStation = new Mock<ILocation>();
            var mockPlanetStation = new Mock<IPlanet>();
            var mockGalaxyStation = new Mock<IGalaxy>();
            mockPlanetStation.SetupGet(mock => mock.Name).Returns("planetStation");
            mockPlanetStation.SetupGet(mock => mock.Galaxy).Returns(mockGalaxyStation.Object);
            mockPlanetStation.SetupGet(mock => mock.Units).Returns(new List<IUnit>());
            mockGalaxyStation.SetupGet(mock => mock.Name).Returns("galaxyStation");
            mockLocationStation.SetupGet(mock => mock.Planet).Returns(mockPlanetStation.Object);

            var mockGalaxy = new Mock<IGalaxy>();
            mockGalaxy.SetupGet(mock => mock.Name).Returns("galaxy");

            var mockPlanet = new Mock<IPlanet>();
            mockPlanet.SetupGet(mock => mock.Name).Returns("planet");
            mockPlanet.SetupGet(mock => mock.Galaxy).Returns(mockGalaxy.Object);
            mockPlanet.SetupGet(mock => mock.Units).Returns(new List<IUnit>() { });

            var mockLocationPath = new Mock<ILocation>();
            mockLocationPath.SetupGet(mock => mock.Planet).Returns(mockPlanet.Object);

            var mockPathToDestination = new Mock<IPath>();
            mockPathToDestination.SetupGet(mock => mock.TargetLocation).Returns(mockLocationPath.Object);
            mockGalacticMap.Add(mockPathToDestination.Object);

            var mockUnitToTeleport = new Mock<IUnit>();
            mockUnitToTeleport.SetupGet(mock => mock.CurrentLocation).Returns(mockLocationStation.Object);
            mockUnitToTeleport.Setup(mock => mock.CanPay(It.IsAny<IResources>())).Returns(true);
            mockUnitToTeleport.Setup(mock => mock.Pay(It.IsAny<IResources>())).Returns(new Mock<IResources>().Object);

            var mockTargetLocation = new Mock<ILocation>();
            mockTargetLocation.SetupGet(mock => mock.Planet).Returns(mockPlanet.Object);
            mockTargetLocation.SetupGet(mock => mock.Coordinates.Longtitude).Returns((double)0);
            mockTargetLocation.SetupGet(mock => mock.Coordinates.Longtitude).Returns((double)0);

            var teleportStation = new TeleportStation(
                 mockOwner.Object, mockGalacticMap, mockLocationStation.Object);

            // Act
            teleportStation.TeleportUnit(mockUnitToTeleport.Object, mockTargetLocation.Object);

            // Assert unit.Pay method was accessed.
            mockUnitToTeleport.Verify(mock => mock.Pay(It.IsAny<IResources>()), Times.Once());
        }

        //TeleportUnit should obtain a payment from the unitToTeleport for the provided services,
        //when all of the validations pass successfully and the unit is ready for teleportation,
        //and as a result - the amount of Resources of the TeleportStation must be increased by the 
        //amount of the payment.
        [Test]
        public void TeleportUnit_ShouldAddResorcesToTeleportStationResources_WhenInputParametersAreValid()
        {
            // Arrange.
            var mockOwner = new Mock<IBusinessOwner>();
            var mockGalacticMap = new List<IPath>();
            var mockLocationStation = new Mock<ILocation>();
            var mockPlanetStation = new Mock<IPlanet>();
            var mockGalaxyStation = new Mock<IGalaxy>();
            mockPlanetStation.SetupGet(mock => mock.Name).Returns("planetStation");
            mockPlanetStation.SetupGet(mock => mock.Galaxy).Returns(mockGalaxyStation.Object);
            mockPlanetStation.SetupGet(mock => mock.Units).Returns(new List<IUnit>());
            mockGalaxyStation.SetupGet(mock => mock.Name).Returns("galaxyStation");
            mockLocationStation.SetupGet(mock => mock.Planet).Returns(mockPlanetStation.Object);

            var mockGalaxy = new Mock<IGalaxy>();
            mockGalaxy.SetupGet(mock => mock.Name).Returns("galaxy");

            var mockPlanet = new Mock<IPlanet>();
            mockPlanet.SetupGet(mock => mock.Name).Returns("planet");
            mockPlanet.SetupGet(mock => mock.Galaxy).Returns(mockGalaxy.Object);
            mockPlanet.SetupGet(mock => mock.Units).Returns(new List<IUnit>() { });

            var mockLocationPath = new Mock<ILocation>();
            mockLocationPath.SetupGet(mock => mock.Planet).Returns(mockPlanet.Object);

            var mockPathToDestination = new Mock<IPath>();
            mockPathToDestination.SetupGet(mock => mock.TargetLocation).Returns(mockLocationPath.Object);
            mockGalacticMap.Add(mockPathToDestination.Object);

            // Arrange the payment.
            var mockResourcesPayment = new Mock<IResources>();
            mockResourcesPayment.SetupGet(mock => mock.GoldCoins).Returns((uint)1);
            mockResourcesPayment.SetupGet(mock => mock.SilverCoins).Returns((uint)2);
            mockResourcesPayment.SetupGet(mock => mock.BronzeCoins).Returns((uint)3);

            var mockUnitToTeleport = new Mock<IUnit>();
            mockUnitToTeleport.SetupGet(mock => mock.CurrentLocation).Returns(mockLocationStation.Object);
            mockUnitToTeleport.Setup(mock => mock.CanPay(It.IsAny<IResources>())).Returns(true);
            mockUnitToTeleport.Setup(mock => mock.Pay(It.IsAny<IResources>())).Returns(mockResourcesPayment.Object);

            var mockTargetLocation = new Mock<ILocation>();
            mockTargetLocation.SetupGet(mock => mock.Planet).Returns(mockPlanet.Object);
            mockTargetLocation.SetupGet(mock => mock.Coordinates.Longtitude).Returns((double)0);
            mockTargetLocation.SetupGet(mock => mock.Coordinates.Longtitude).Returns((double)0);

            var teleportStation = new ExposedProtectedFieldsMockTeleportStation(
                 mockOwner.Object, mockGalacticMap, mockLocationStation.Object);

            teleportStation.TeleportUnit(mockUnitToTeleport.Object, mockTargetLocation.Object);

            // Act
            var actualTeleportStationResources = teleportStation.Resources;

            // Assert that TeleportationStation has the same amount of resources as were received by the payment.
            Assert.That(actualTeleportStationResources,
                Is.InstanceOf<IResources>()
                .With.Property("GoldCoins").EqualTo((uint)1)
                .And.Property("SilverCoins").EqualTo((uint)2)
                .And.Property("BronzeCoins").EqualTo((uint)3));
        }

        //TeleportUnit should Set the unitToTeleport's previous location to unitToTeleport's 
        //current location, when all of the validations pass successfully and the unit is being teleported.
        [Test]
        public void TeleportUnit_ShouldSetUnitToTeleportPreviousLocationToUnitToTeleportCurrentLocation_WhenInputParametersAreValid()
        {
            // Arrange.
            var mockOwner = new Mock<IBusinessOwner>();
            var mockGalacticMap = new List<IPath>();
            var mockLocationStation = new Mock<ILocation>();
            var mockPlanetStation = new Mock<IPlanet>();
            var mockGalaxyStation = new Mock<IGalaxy>();
            mockPlanetStation.SetupGet(mock => mock.Name).Returns("planetStation");
            mockPlanetStation.SetupGet(mock => mock.Galaxy).Returns(mockGalaxyStation.Object);
            mockPlanetStation.SetupGet(mock => mock.Units).Returns(new List<IUnit>());
            mockGalaxyStation.SetupGet(mock => mock.Name).Returns("galaxyStation");
            mockLocationStation.SetupGet(mock => mock.Planet).Returns(mockPlanetStation.Object);

            var mockGalaxy = new Mock<IGalaxy>();
            mockGalaxy.SetupGet(mock => mock.Name).Returns("galaxy");

            var mockPlanet = new Mock<IPlanet>();
            mockPlanet.SetupGet(mock => mock.Name).Returns("planet");
            mockPlanet.SetupGet(mock => mock.Galaxy).Returns(mockGalaxy.Object);
            mockPlanet.SetupGet(mock => mock.Units).Returns(new List<IUnit>() { });

            var mockLocationPath = new Mock<ILocation>();
            mockLocationPath.SetupGet(mock => mock.Planet).Returns(mockPlanet.Object);

            var mockPathToDestination = new Mock<IPath>();
            mockPathToDestination.SetupGet(mock => mock.TargetLocation).Returns(mockLocationPath.Object);
            mockGalacticMap.Add(mockPathToDestination.Object);

            var mockResourcesPayment = new Mock<IResources>();
            mockResourcesPayment.SetupGet(mock => mock.GoldCoins).Returns((uint)1);
            mockResourcesPayment.SetupGet(mock => mock.SilverCoins).Returns((uint)2);
            mockResourcesPayment.SetupGet(mock => mock.BronzeCoins).Returns((uint)3);

            var mockUnitToTeleport = new Mock<IUnit>();
            mockUnitToTeleport.SetupGet(mock => mock.CurrentLocation).Returns(mockLocationStation.Object);
            mockUnitToTeleport.Setup(mock => mock.CanPay(It.IsAny<IResources>())).Returns(true);
            mockUnitToTeleport.Setup(mock => mock.Pay(It.IsAny<IResources>())).Returns(mockResourcesPayment.Object);

            var mockTargetLocation = new Mock<ILocation>();
            mockTargetLocation.SetupGet(mock => mock.Planet).Returns(mockPlanet.Object);
            mockTargetLocation.SetupGet(mock => mock.Coordinates.Longtitude).Returns((double)0);
            mockTargetLocation.SetupGet(mock => mock.Coordinates.Longtitude).Returns((double)0);

            var teleportStation = new ExposedProtectedFieldsMockTeleportStation(
                 mockOwner.Object, mockGalacticMap, mockLocationStation.Object);

            // Act.
            teleportStation.TeleportUnit(mockUnitToTeleport.Object, mockTargetLocation.Object);

            // Assert that unitToTeleport.PreviousLocation setter was accessed with the correct value and the correct amount of times.
            mockUnitToTeleport.VerifySet(mock => mock.PreviousLocation = mockLocationStation.Object, Times.Once());
        }

        //TeleportUnit should Set the unitToTeleport's current location to targetLocation, 
        //when all of the validations pass successfully and the unit is being teleported.
        [Test]
        public void TeleportUnit_ShouldSetUnitToTeleportCurretLocationToTargeLocation_WhenInputParamteresAreValid()
        {
            // Arrange.
            var mockOwner = new Mock<IBusinessOwner>();
            var mockGalacticMap = new List<IPath>();
            var mockLocationStation = new Mock<ILocation>();
            var mockPlanetStation = new Mock<IPlanet>();
            var mockGalaxyStation = new Mock<IGalaxy>();
            mockPlanetStation.SetupGet(mock => mock.Name).Returns("planetStation");
            mockPlanetStation.SetupGet(mock => mock.Galaxy).Returns(mockGalaxyStation.Object);
            mockPlanetStation.SetupGet(mock => mock.Units).Returns(new List<IUnit>());
            mockGalaxyStation.SetupGet(mock => mock.Name).Returns("galaxyStation");
            mockLocationStation.SetupGet(mock => mock.Planet).Returns(mockPlanetStation.Object);

            var mockGalaxy = new Mock<IGalaxy>();
            mockGalaxy.SetupGet(mock => mock.Name).Returns("galaxy");

            var mockPlanet = new Mock<IPlanet>();
            mockPlanet.SetupGet(mock => mock.Name).Returns("planet");
            mockPlanet.SetupGet(mock => mock.Galaxy).Returns(mockGalaxy.Object);
            mockPlanet.SetupGet(mock => mock.Units).Returns(new List<IUnit>() { });

            var mockLocationPath = new Mock<ILocation>();
            mockLocationPath.SetupGet(mock => mock.Planet).Returns(mockPlanet.Object);

            var mockPathToDestination = new Mock<IPath>();
            mockPathToDestination.SetupGet(mock => mock.TargetLocation).Returns(mockLocationPath.Object);
            mockGalacticMap.Add(mockPathToDestination.Object);

            var mockResourcesPayment = new Mock<IResources>();
            mockResourcesPayment.SetupGet(mock => mock.GoldCoins).Returns((uint)1);
            mockResourcesPayment.SetupGet(mock => mock.SilverCoins).Returns((uint)2);
            mockResourcesPayment.SetupGet(mock => mock.BronzeCoins).Returns((uint)3);

            var mockUnitToTeleport = new Mock<IUnit>();
            mockUnitToTeleport.SetupGet(mock => mock.CurrentLocation).Returns(mockLocationStation.Object);
            mockUnitToTeleport.Setup(mock => mock.CanPay(It.IsAny<IResources>())).Returns(true);
            mockUnitToTeleport.Setup(mock => mock.Pay(It.IsAny<IResources>())).Returns(mockResourcesPayment.Object);

            var mockTargetLocation = new Mock<ILocation>();
            mockTargetLocation.SetupGet(mock => mock.Planet).Returns(mockPlanet.Object);
            mockTargetLocation.SetupGet(mock => mock.Coordinates.Longtitude).Returns((double)0);
            mockTargetLocation.SetupGet(mock => mock.Coordinates.Longtitude).Returns((double)0);

            var teleportStation = new ExposedProtectedFieldsMockTeleportStation(
                 mockOwner.Object, mockGalacticMap, mockLocationStation.Object);

            // Act.
            teleportStation.TeleportUnit(mockUnitToTeleport.Object, mockTargetLocation.Object);

            // Assert that unitToTeleport CurrentLocation was accessed with the correct value.
            mockUnitToTeleport.VerifySet(mock => mock.CurrentLocation = mockTargetLocation.Object, Times.Once());
        }

        //TeleportUnit should Add the unitToTeleport to the list of Units of the 
        //targetLocation (Planet.Units), when all of the validations pass successfully 
        //and the unit is on its way to being teleported.
        [Test]
        public void TeleportUnit_ShouldAddUnitToTeleportToTargetLocationPlanetUnits_WhenInputParametersAreValid()
        {
            // Arrange
            var mockOwner = new Mock<IBusinessOwner>();
            var mockGalacticMap = new List<IPath>();
            var mockLocationStation = new Mock<ILocation>();
            var mockPlanetStation = new Mock<IPlanet>();
            var mockGalaxyStation = new Mock<IGalaxy>();
            mockPlanetStation.SetupGet(mock => mock.Name).Returns("planetStation");
            mockPlanetStation.SetupGet(mock => mock.Galaxy).Returns(mockGalaxyStation.Object);
            mockPlanetStation.SetupGet(mock => mock.Units).Returns(new List<IUnit>());
            mockGalaxyStation.SetupGet(mock => mock.Name).Returns("galaxyStation");
            mockLocationStation.SetupGet(mock => mock.Planet).Returns(mockPlanetStation.Object);

            var mockGalaxy = new Mock<IGalaxy>();
            mockGalaxy.SetupGet(mock => mock.Name).Returns("galaxy");

            // Arrange the list of units to assert against.
            var mockPlanetListOfUnits = new List<IUnit>();
            var mockPlanet = new Mock<IPlanet>();
            mockPlanet.SetupGet(mock => mock.Name).Returns("planet");
            mockPlanet.SetupGet(mock => mock.Galaxy).Returns(mockGalaxy.Object);
            mockPlanet.SetupGet(mock => mock.Units).Returns(mockPlanetListOfUnits);

            var mockLocationPath = new Mock<ILocation>();
            mockLocationPath.SetupGet(mock => mock.Planet).Returns(mockPlanet.Object);

            var mockPathToDestination = new Mock<IPath>();
            mockPathToDestination.SetupGet(mock => mock.TargetLocation).Returns(mockLocationPath.Object);
            mockGalacticMap.Add(mockPathToDestination.Object);

            var mockResourcesPayment = new Mock<IResources>();
            mockResourcesPayment.SetupGet(mock => mock.GoldCoins).Returns((uint)1);
            mockResourcesPayment.SetupGet(mock => mock.SilverCoins).Returns((uint)2);
            mockResourcesPayment.SetupGet(mock => mock.BronzeCoins).Returns((uint)3);

            var mockUnitToTeleport = new Mock<IUnit>();
            mockUnitToTeleport.SetupGet(mock => mock.CurrentLocation).Returns(mockLocationStation.Object);
            mockUnitToTeleport.Setup(mock => mock.CanPay(It.IsAny<IResources>())).Returns(true);
            mockUnitToTeleport.Setup(mock => mock.Pay(It.IsAny<IResources>())).Returns(mockResourcesPayment.Object);

            var mockTargetLocation = new Mock<ILocation>();
            mockTargetLocation.SetupGet(mock => mock.Planet).Returns(mockPlanet.Object);
            mockTargetLocation.SetupGet(mock => mock.Coordinates.Longtitude).Returns((double)0);
            mockTargetLocation.SetupGet(mock => mock.Coordinates.Longtitude).Returns((double)0);

            var teleportStation = new ExposedProtectedFieldsMockTeleportStation(
                 mockOwner.Object, mockGalacticMap, mockLocationStation.Object);

            // Act.
            teleportStation.TeleportUnit(mockUnitToTeleport.Object, mockTargetLocation.Object);

            // Assert against the provided list.
            CollectionAssert.Contains(mockPlanetListOfUnits, mockUnitToTeleport.Object);
        }

        //TeleportUnit should Remove the unitToTeleport from the list of Units of the 
        //unit's current location (Planet.Units), when all of the validations pass successfully 
        //and the unit is on its way to being teleported.
        [Test]
        public void TeleportUnit_ShouldRemoveUnitToTeleportFromCurrentLocationUnits_WhenInputParametersAreValid()
        {
            // Arrange.
            var mockOwner = new Mock<IBusinessOwner>();
            var mockGalacticMap = new List<IPath>();
            var mockLocationStation = new Mock<ILocation>();
            var mockPlanetStation = new Mock<IPlanet>();
            var mockGalaxyStation = new Mock<IGalaxy>();
            var mockStationUnits = new List<IUnit>();
            mockPlanetStation.SetupGet(mock => mock.Name).Returns("planetStation");
            mockPlanetStation.SetupGet(mock => mock.Galaxy).Returns(mockGalaxyStation.Object);
            mockPlanetStation.SetupGet(mock => mock.Units).Returns(mockStationUnits);
            mockGalaxyStation.SetupGet(mock => mock.Name).Returns("galaxyStation");
            mockLocationStation.SetupGet(mock => mock.Planet).Returns(mockPlanetStation.Object);

            var mockGalaxy = new Mock<IGalaxy>();
            mockGalaxy.SetupGet(mock => mock.Name).Returns("galaxy");

            var mockPlanetListOfUnits = new List<IUnit>();
            var mockPlanet = new Mock<IPlanet>();
            mockPlanet.SetupGet(mock => mock.Name).Returns("planet");
            mockPlanet.SetupGet(mock => mock.Galaxy).Returns(mockGalaxy.Object);
            mockPlanet.SetupGet(mock => mock.Units).Returns(mockPlanetListOfUnits);

            var mockLocationPath = new Mock<ILocation>();
            mockLocationPath.SetupGet(mock => mock.Planet).Returns(mockPlanet.Object);

            var mockPathToDestination = new Mock<IPath>();
            mockPathToDestination.SetupGet(mock => mock.TargetLocation).Returns(mockLocationPath.Object);
            mockGalacticMap.Add(mockPathToDestination.Object);

            var mockResourcesPayment = new Mock<IResources>();
            mockResourcesPayment.SetupGet(mock => mock.GoldCoins).Returns((uint)1);
            mockResourcesPayment.SetupGet(mock => mock.SilverCoins).Returns((uint)2);
            mockResourcesPayment.SetupGet(mock => mock.BronzeCoins).Returns((uint)3);

            var mockUnitToTeleport = new Mock<IUnit>();
            mockUnitToTeleport.SetupGet(mock => mock.CurrentLocation).Returns(mockLocationStation.Object);
            mockUnitToTeleport.Setup(mock => mock.CanPay(It.IsAny<IResources>())).Returns(true);
            mockUnitToTeleport.Setup(mock => mock.Pay(It.IsAny<IResources>())).Returns(mockResourcesPayment.Object);

            var mockTargetLocation = new Mock<ILocation>();
            mockTargetLocation.SetupGet(mock => mock.Planet).Returns(mockPlanet.Object);
            mockTargetLocation.SetupGet(mock => mock.Coordinates.Longtitude).Returns((double)0);
            mockTargetLocation.SetupGet(mock => mock.Coordinates.Longtitude).Returns((double)0);

            var teleportStation = new ExposedProtectedFieldsMockTeleportStation(
                 mockOwner.Object, mockGalacticMap, mockLocationStation.Object);

            // Arrange the unit to be removed
            mockStationUnits.Add(mockUnitToTeleport.Object);

            // Act.
            teleportStation.TeleportUnit(mockUnitToTeleport.Object, mockTargetLocation.Object);

            // Assert the unit was actually removed.
            CollectionAssert.DoesNotContain(mockStationUnits, mockUnitToTeleport.Object);
        }

        //PayProfits should return the total amount of profits(Resources)
        //generated using the TeleportUnit service, when the argument passed 
        //represents the actual owner of the TeleportStation.
        [Test]
        public void PayProfits_ShouldReturnResources_WhenInputParametersAreValid()
        {
            // Arrange
            var mockBusinessOwner = new Mock<IBusinessOwner>();
            mockBusinessOwner.SetupGet(mock => mock.IdentificationNumber).Returns(1);
            var mockGalacticMap = new Mock<IEnumerable<IPath>>();
            var mockLocation = new Mock<ILocation>();

            var teleportStation = new ExposedProtectedFieldsMockTeleportStation(
                mockBusinessOwner.Object, mockGalacticMap.Object, mockLocation.Object);
            
            teleportStation.Resources.GoldCoins = 10;
            teleportStation.Resources.SilverCoins = 11;
            teleportStation.Resources.BronzeCoins = 12;

            // Act
            var actualPayment = teleportStation.PayProfits(mockBusinessOwner.Object);

            // Assert.
            Assert.That(actualPayment,
                Is.InstanceOf<IResources>()
                .With.Property("GoldCoins").EqualTo((uint)10)
                .And.Property("SilverCoins").EqualTo((uint)11)
                .And.Property("BronzeCoins").EqualTo((uint)12));
        }
    }
}
