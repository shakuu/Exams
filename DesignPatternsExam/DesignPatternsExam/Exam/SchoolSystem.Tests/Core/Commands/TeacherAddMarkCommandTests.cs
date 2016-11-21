using System.Collections.Generic;

using NUnit.Framework;
using Moq;

using SchoolSystem.Framework.Core;
using SchoolSystem.Framework.Core.Commands;
using SchoolSystem.Framework.Models.Contracts;
using System;
using SchoolSystem.Framework.Models.Enums;

namespace SchoolSystem.Tests.Core.Commands
{
    [TestFixture]
    public class TeacherAddMarkCommandTests
    {
        [Test]
        public void Execute_ShouldThrowArgumentOutOfRangeException_WhenParametersAreLessThanThree()
        {
            var mockSchoolSystemData = new Mock<ISchoolSystemData>();
            var mockStudentsDataCollection = new Mock<ISchoolSystemDataCollection<IStudent>>();
            var mockTeachersDataCollection = new Mock<ISchoolSystemDataCollection<ITeacher>>();

            var mockStudent = new Mock<IStudent>();
            var mockTeacher = new Mock<ITeacher>();

            mockSchoolSystemData.SetupGet(ssd => ssd.Students).Returns(mockStudentsDataCollection.Object);
            mockSchoolSystemData.SetupGet(ssd => ssd.Teachers).Returns(mockTeachersDataCollection.Object);

            mockStudentsDataCollection.Setup(sdc => sdc.GetById(It.IsAny<int>())).Returns(mockStudent.Object);
            mockTeachersDataCollection.Setup(tdc => tdc.GetById(It.IsAny<int>())).Returns(mockTeacher.Object);

            var parameters = new List<string>()
            {
                "1",
                "1"
            };

            var teacherAddMarkCommand = new TeacherAddMarkCommand();

            Assert.That(
                () => teacherAddMarkCommand.Execute(parameters, mockSchoolSystemData.Object),
                Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [TestCase("one", "2", "3.3")]
        [TestCase("1", "two", "3.3")]
        [TestCase("1", "2", "three-point-three")]
        public void Execute_ShouldThrowFormatException_WhenParametersCannotBeParsed(string firstParameter, string secondParameter, string thirdParameter)
        {
            var mockSchoolSystemData = new Mock<ISchoolSystemData>();
            var mockStudentsDataCollection = new Mock<ISchoolSystemDataCollection<IStudent>>();
            var mockTeachersDataCollection = new Mock<ISchoolSystemDataCollection<ITeacher>>();

            var mockStudent = new Mock<IStudent>();
            var mockTeacher = new Mock<ITeacher>();

            mockSchoolSystemData.SetupGet(ssd => ssd.Students).Returns(mockStudentsDataCollection.Object);
            mockSchoolSystemData.SetupGet(ssd => ssd.Teachers).Returns(mockTeachersDataCollection.Object);

            mockStudentsDataCollection.Setup(sdc => sdc.GetById(It.IsAny<int>())).Returns(mockStudent.Object);
            mockTeachersDataCollection.Setup(tdc => tdc.GetById(It.IsAny<int>())).Returns(mockTeacher.Object);

            var parameters = new List<string>()
            {
                firstParameter,
                secondParameter,
                thirdParameter
            };

            var teacherAddMarkCommand = new TeacherAddMarkCommand();

            Assert.That(
                () => teacherAddMarkCommand.Execute(parameters, mockSchoolSystemData.Object),
                Throws.InstanceOf<FormatException>());
        }

        [Test]
        public void Execute_ShouldInvokeSchoolSystemDataStudentsCollection_GetByIdWithCorrectParameter()
        {
            var mockSchoolSystemData = new Mock<ISchoolSystemData>();
            var mockStudentsDataCollection = new Mock<ISchoolSystemDataCollection<IStudent>>();
            var mockTeachersDataCollection = new Mock<ISchoolSystemDataCollection<ITeacher>>();

            var mockStudent = new Mock<IStudent>();
            var mockTeacher = new Mock<ITeacher>();

            mockSchoolSystemData.SetupGet(ssd => ssd.Students).Returns(mockStudentsDataCollection.Object);
            mockSchoolSystemData.SetupGet(ssd => ssd.Teachers).Returns(mockTeachersDataCollection.Object);

            mockStudentsDataCollection.Setup(sdc => sdc.GetById(It.IsAny<int>())).Returns(mockStudent.Object);
            mockTeachersDataCollection.Setup(tdc => tdc.GetById(It.IsAny<int>())).Returns(mockTeacher.Object);

            var parameters = new List<string>()
            {
                "1",
                "2",
                "3.3"
            };

            var teacherAddMarkCommand = new TeacherAddMarkCommand();
            teacherAddMarkCommand.Execute(parameters, mockSchoolSystemData.Object);

            mockStudentsDataCollection.Verify(sdc => sdc.GetById(2), Times.Once());
        }

        [Test]
        public void Execute_ShouldInvokeSchoolSystemDataTeachersCollection_GetByIdWithCorrectParameter()
        {
            var mockSchoolSystemData = new Mock<ISchoolSystemData>();
            var mockStudentsDataCollection = new Mock<ISchoolSystemDataCollection<IStudent>>();
            var mockTeachersDataCollection = new Mock<ISchoolSystemDataCollection<ITeacher>>();

            var mockStudent = new Mock<IStudent>();
            var mockTeacher = new Mock<ITeacher>();

            mockSchoolSystemData.SetupGet(ssd => ssd.Students).Returns(mockStudentsDataCollection.Object);
            mockSchoolSystemData.SetupGet(ssd => ssd.Teachers).Returns(mockTeachersDataCollection.Object);

            mockStudentsDataCollection.Setup(sdc => sdc.GetById(It.IsAny<int>())).Returns(mockStudent.Object);
            mockTeachersDataCollection.Setup(tdc => tdc.GetById(It.IsAny<int>())).Returns(mockTeacher.Object);

            var parameters = new List<string>()
            {
                "1",
                "2",
                "3.3"
            };

            var teacherAddMarkCommand = new TeacherAddMarkCommand();
            teacherAddMarkCommand.Execute(parameters, mockSchoolSystemData.Object);

            mockTeachersDataCollection.Verify(tdc => tdc.GetById(1), Times.Once());
        }

        [Test]
        public void Execute_ShouldInvokeTeacherAddMark_WithCorrectParameters()
        {
            var mockSchoolSystemData = new Mock<ISchoolSystemData>();
            var mockStudentsDataCollection = new Mock<ISchoolSystemDataCollection<IStudent>>();
            var mockTeachersDataCollection = new Mock<ISchoolSystemDataCollection<ITeacher>>();

            var mockStudent = new Mock<IStudent>();
            var mockTeacher = new Mock<ITeacher>();

            mockSchoolSystemData.SetupGet(ssd => ssd.Students).Returns(mockStudentsDataCollection.Object);
            mockSchoolSystemData.SetupGet(ssd => ssd.Teachers).Returns(mockTeachersDataCollection.Object);

            mockStudentsDataCollection.Setup(sdc => sdc.GetById(It.IsAny<int>())).Returns(mockStudent.Object);
            mockTeachersDataCollection.Setup(tdc => tdc.GetById(It.IsAny<int>())).Returns(mockTeacher.Object);

            var parameters = new List<string>()
            {
                "1",
                "2",
                "3.3"
            };

            var teacherAddMarkCommand = new TeacherAddMarkCommand();
            teacherAddMarkCommand.Execute(parameters, mockSchoolSystemData.Object);

            mockTeacher.Verify(t => t.AddMark(mockStudent.Object, 3.3f), Times.Once());
        }

        [Test]
        public void Execute_ShouldReturnCorrectString()
        {
            var mockSchoolSystemData = new Mock<ISchoolSystemData>();
            var mockStudentsDataCollection = new Mock<ISchoolSystemDataCollection<IStudent>>();
            var mockTeachersDataCollection = new Mock<ISchoolSystemDataCollection<ITeacher>>();

            var mockStudent = new Mock<IStudent>();
            var mockTeacher = new Mock<ITeacher>();

            mockTeacher.SetupGet(t => t.FirstName).Returns("teacher-first");
            mockTeacher.SetupGet(t => t.LastName).Returns("teacher-last");
            mockTeacher.SetupGet(t => t.Subject).Returns(Subject.Programming);

            mockStudent.SetupGet(s => s.FirstName).Returns("student-first");
            mockStudent.SetupGet(s => s.LastName).Returns("student-last");

            mockSchoolSystemData.SetupGet(ssd => ssd.Students).Returns(mockStudentsDataCollection.Object);
            mockSchoolSystemData.SetupGet(ssd => ssd.Teachers).Returns(mockTeachersDataCollection.Object);

            mockStudentsDataCollection.Setup(sdc => sdc.GetById(It.IsAny<int>())).Returns(mockStudent.Object);
            mockTeachersDataCollection.Setup(tdc => tdc.GetById(It.IsAny<int>())).Returns(mockTeacher.Object);

            var parameters = new List<string>()
            {
                "1",
                "2",
                "3.3"
            };

            var mark = 3.3f;
            var expectedString = $"Teacher {mockTeacher.Object.FirstName} {mockTeacher.Object.LastName} added mark {mark} to student {mockStudent.Object.FirstName} {mockStudent.Object.LastName} in {mockTeacher.Object.Subject}.";

            var teacherAddMarkCommand = new TeacherAddMarkCommand();
            var actualString = teacherAddMarkCommand.Execute(parameters, mockSchoolSystemData.Object);

            Assert.True(actualString == expectedString);
        }
    }
}
