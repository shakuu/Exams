using System;
using System.Collections.Generic;

using NUnit.Framework;
using Moq;

using SchoolSystem.Framework.Core;
using SchoolSystem.Framework.Core.Commands;
using SchoolSystem.Framework.Models.Contracts;

namespace SchoolSystem.Tests.Core.Commands
{
    [TestFixture]
    public class RemoveStudentCommandTests
    {
        [Test]
        public void Execute_ShouldInvoke_SchoolSystemDataStudentsRemoveMethodWithCorrectId()
        {
            var mockSchoolSystemData = new Mock<ISchoolSystemData>();
            var mockStudentsDataCollection = new Mock<ISchoolSystemDataCollection<IStudent>>();
            mockSchoolSystemData.SetupGet(ssd => ssd.Students).Returns(mockStudentsDataCollection.Object);
            mockSchoolSystemData.SetupGet(ssd => ssd.Teachers).Returns(new Mock<ISchoolSystemDataCollection<ITeacher>>().Object);
            
            var parameters = new List<string>() { "42" };
            var removeStudentCommand = new RemoveStudentCommand();
            removeStudentCommand.Execute(parameters, mockSchoolSystemData.Object);

            mockStudentsDataCollection.Verify(ssd => ssd.Remove(42), Times.Once());
        }

        [Test]
        public void Execute_ShouldReturnCorrectString()
        {
            var mockSchoolSystemData = new Mock<ISchoolSystemData>();
            mockSchoolSystemData.SetupGet(ssd => ssd.Students).Returns(new Mock<ISchoolSystemDataCollection<IStudent>>().Object);
            mockSchoolSystemData.SetupGet(ssd => ssd.Teachers).Returns(new Mock<ISchoolSystemDataCollection<ITeacher>>().Object);

            var parameters = new List<string>() { "42" };
            var removeStudentCommand = new RemoveStudentCommand();

            var expectedString = $"Student with ID {"42"} was sucessfully removed.";
            var actualString = removeStudentCommand.Execute(parameters, mockSchoolSystemData.Object);

            Assert.True(actualString == expectedString);
        }

        [Test]
        public void Execute_ShouldThrow_WhenParameterCannotBeParsedToInt()
        {
            var mockSchoolSystemData = new Mock<ISchoolSystemData>();
            mockSchoolSystemData.SetupGet(ssd => ssd.Students).Returns(new Mock<ISchoolSystemDataCollection<IStudent>>().Object);
            mockSchoolSystemData.SetupGet(ssd => ssd.Teachers).Returns(new Mock<ISchoolSystemDataCollection<ITeacher>>().Object);

            var parameters = new List<string>() { "forty-two" };
            var removeStudentCommand = new RemoveStudentCommand();

            Assert.That(
                () => removeStudentCommand.Execute(parameters, mockSchoolSystemData.Object),
                Throws.InstanceOf<FormatException>());
        }
    }
}
