using System;
using System.Collections.Generic;

using SchoolSystem.Framework.Core.Commands.Contracts;
using SchoolSystem.Framework.Core.Contracts;
using SchoolSystem.Framework.Core.Factories;
using SchoolSystem.Framework.Models.Enums;

namespace SchoolSystem.Framework.Core.Commands
{
    public class CreateStudentCommand : ICommand
    {
        private readonly IStudentFactory studentFactory;
        private readonly IIdentityProvider idProvider;
        private readonly IStudentData studentData;

        public CreateStudentCommand(IStudentFactory studentFactory, IStudentData studentData, IIdentityProvider idProvider)
        {
            if (studentFactory == null)
            {
                throw new ArgumentNullException(nameof(studentFactory));
            }

            if (idProvider == null)
            {
                throw new ArgumentNullException(nameof(idProvider));
            }

            this.idProvider = idProvider;
            this.studentFactory = studentFactory;
            this.studentData = studentData;
        }

        public string Execute(IList<string> parameters, ISchoolSystemData schoolSystemData)
        {
            var firstName = parameters[0];
            var lastName = parameters[1];
            var grade = (Grade)int.Parse(parameters[2]);

            var student = this.studentFactory.CreateStudent(firstName, lastName, grade);

            var nextId = this.idProvider.NextId();
            this.studentData.Students.Add(nextId, student);

            return $"A new student with name {firstName} {lastName}, grade {grade} and ID {nextId} was created.";
        }
    }
}
