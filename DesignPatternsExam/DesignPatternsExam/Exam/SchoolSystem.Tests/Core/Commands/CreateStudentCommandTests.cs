using System;
using System.Collections.Generic;

using NUnit.Framework;
using Moq;

using SchoolSystem.Framework.Core;
using SchoolSystem.Framework.Core.Commands;
using SchoolSystem.Framework.Core.Contracts;
using SchoolSystem.Framework.Core.Factories;
using SchoolSystem.Framework.Models.Contracts;
using SchoolSystem.Framework.Models.Enums;
using SchoolSystem.Framework.Core.Providers;

namespace SchoolSystem.Tests.Core.Commands
{
    [TestFixture]
    public class CreateStudentCommandTests
    {
        [Test]
        public void Execute_ShouldInvoke_StudentFactoryCreateStudent_WithCorrectParameters()
        {
            var mockStudentFactory = new Mock<IStudentFactory>();
            var stubIdProvider = new Mock<IIdentityProvider>();
            stubIdProvider.Setup(id => id.NextId()).Returns(0);

            var stubSchoolSystemData = new Mock<ISchoolSystemData>();
            stubSchoolSystemData.SetupGet(ssd => ssd.Students).Returns(new Mock<ISchoolSystemDataCollection<IStudent>>().Object);
            stubSchoolSystemData.SetupGet(ssd => ssd.Teachers).Returns(new Mock<ISchoolSystemDataCollection<ITeacher>>().Object);

            var commandString = new List<string>() { "Pesho", "Peshev", "11" };
            var createStudentCommand = new CreateStudentCommand(mockStudentFactory.Object, stubIdProvider.Object);

            createStudentCommand.Execute(commandString, stubSchoolSystemData.Object);

            mockStudentFactory.Verify(sf => sf.CreateStudent(commandString[0], commandString[1], (Grade)int.Parse(commandString[2])), Times.Once());
        }

        [Test]
        public void Execute_ShouldInvoke_IdentityProvider_NextIdMethod_Once()
        {
            var stubStudentFactory = new Mock<IStudentFactory>();
            var mockIdProvider = new Mock<IIdentityProvider>();
            mockIdProvider.Setup(id => id.NextId()).Returns(0);

            var stubSchoolSystemData = new Mock<ISchoolSystemData>();
            stubSchoolSystemData.SetupGet(ssd => ssd.Students).Returns(new Mock<ISchoolSystemDataCollection<IStudent>>().Object);
            stubSchoolSystemData.SetupGet(ssd => ssd.Teachers).Returns(new Mock<ISchoolSystemDataCollection<ITeacher>>().Object);

            var commandString = new List<string>() { "Pesho", "Peshev", "11" };
            var createStudentCommand = new CreateStudentCommand(stubStudentFactory.Object, mockIdProvider.Object);

            createStudentCommand.Execute(commandString, stubSchoolSystemData.Object);

            mockIdProvider.Verify(id => id.NextId(), Times.Once());
        }

        [Test]
        public void Execute_ShouldIncremnt_IdentityProvider_Value()
        {
            var stubStudentFactory = new Mock<IStudentFactory>();

            var idProvider = new SchoolSystemIdentityProvider();

            var stubSchoolSystemData = new Mock<ISchoolSystemData>();
            var mockStudentsCollection = new Mock<ISchoolSystemDataCollection<IStudent>>();
            stubSchoolSystemData.SetupGet(ssd => ssd.Students).Returns(mockStudentsCollection.Object);
            stubSchoolSystemData.SetupGet(ssd => ssd.Teachers).Returns(new Mock<ISchoolSystemDataCollection<ITeacher>>().Object);

            var commandString = new List<string>() { "Pesho", "Peshev", "11" };
            var createStudentCommand = new CreateStudentCommand(stubStudentFactory.Object, idProvider);

            createStudentCommand.Execute(commandString, stubSchoolSystemData.Object);
            createStudentCommand.Execute(commandString, stubSchoolSystemData.Object);

            // Incrementing IdProvider.
            mockStudentsCollection.Verify(sc => sc.Add(0, It.IsAny<IStudent>()), Times.Once());
            mockStudentsCollection.Verify(sc => sc.Add(1, It.IsAny<IStudent>()), Times.Once());
        }

        [Test]
        public void Execute_ShouldReturnCorrectString()
        {
            var stubStudentFactory = new Mock<IStudentFactory>();
            var stubIdProvider = new Mock<IIdentityProvider>();
            stubIdProvider.Setup(id => id.NextId()).Returns(0);

            var stubSchoolSystemData = new Mock<ISchoolSystemData>();
            stubSchoolSystemData.SetupGet(ssd => ssd.Students).Returns(new Mock<ISchoolSystemDataCollection<IStudent>>().Object);
            stubSchoolSystemData.SetupGet(ssd => ssd.Teachers).Returns(new Mock<ISchoolSystemDataCollection<ITeacher>>().Object);

            var commandString = new List<string>() { "Pesho", "Peshev", "11" };
            var createStudentCommand = new CreateStudentCommand(stubStudentFactory.Object, stubIdProvider.Object);

            var expectedString = $"A new student with name {"Pesho"} {"Peshev"}, grade {(Grade)int.Parse("11")} and ID {0} was created.";
            var actualString = createStudentCommand.Execute(commandString, stubSchoolSystemData.Object);

            Assert.True(expectedString == actualString);
        }

        [Test]
        public void Execute_ShouldThrow_WhenParametersCountIsLessThanThree()
        {
            var stubStudentFactory = new Mock<IStudentFactory>();
            var stubIdProvider = new Mock<IIdentityProvider>();
            stubIdProvider.Setup(id => id.NextId()).Returns(0);

            var stubSchoolSystemData = new Mock<ISchoolSystemData>();
            stubSchoolSystemData.SetupGet(ssd => ssd.Students).Returns(new Mock<ISchoolSystemDataCollection<IStudent>>().Object);
            stubSchoolSystemData.SetupGet(ssd => ssd.Teachers).Returns(new Mock<ISchoolSystemDataCollection<ITeacher>>().Object);

            var commandString = new List<string>() { "Pesho", "Peshev" };
            var createStudentCommand = new CreateStudentCommand(stubStudentFactory.Object, stubIdProvider.Object);

            Assert.That(
                () => createStudentCommand.Execute(commandString, stubSchoolSystemData.Object),
                Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Execute_ShouldThrow_WhenGradeCannotBeParsed()
        {
            var stubStudentFactory = new Mock<IStudentFactory>();
            var stubIdProvider = new Mock<IIdentityProvider>();
            stubIdProvider.Setup(id => id.NextId()).Returns(0);

            var stubSchoolSystemData = new Mock<ISchoolSystemData>();
            stubSchoolSystemData.SetupGet(ssd => ssd.Students).Returns(new Mock<ISchoolSystemDataCollection<IStudent>>().Object);
            stubSchoolSystemData.SetupGet(ssd => ssd.Teachers).Returns(new Mock<ISchoolSystemDataCollection<ITeacher>>().Object);

            var commandString = new List<string>() { "Pesho", "Peshev", "forty-two" };
            var createStudentCommand = new CreateStudentCommand(stubStudentFactory.Object, stubIdProvider.Object);

            Assert.That(
                () => createStudentCommand.Execute(commandString, stubSchoolSystemData.Object),
                Throws.InstanceOf<FormatException>());
        }
    }
}
