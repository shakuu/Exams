using System;
using System.Collections.Generic;

using SchoolSystem.Framework.Core.Commands.Contracts;
using SchoolSystem.Framework.Core.Factories;
using SchoolSystem.Framework.Models.Enums;
using SchoolSystem.Framework.Core.Contracts;

namespace SchoolSystem.Framework.Core.Commands
{
    public class CreateTeacherCommand : ICommand
    {
        private readonly ITeacherFactory teacherFactory;
        private readonly IIdentityProvider idProvider;
        private readonly ITeachersData teachersData;

        public CreateTeacherCommand(ITeacherFactory teacherFactory, ITeachersData teachersData, IIdentityProvider idProvider)
        {
            if (teacherFactory == null)
            {
                throw new ArgumentNullException(nameof(teacherFactory));
            }

            if (idProvider == null)
            {
                throw new ArgumentNullException(nameof(idProvider));
            }

            this.idProvider = idProvider;
            this.teacherFactory = teacherFactory;
            this.teachersData = teachersData;
        }

        public string Execute(IList<string> parameters, ISchoolSystemData schoolSystemData)
        {
            var firstName = parameters[0];
            var lastName = parameters[1];
            var subject = (Subject)int.Parse(parameters[2]);

            var teacher = this.teacherFactory.CreateTeacher(firstName, lastName, subject);

            var nextId = this.idProvider.NextId();
            schoolSystemData.Teachers.Add(nextId, teacher);

            return $"A new teacher with name {firstName} {lastName}, subject {subject} and ID {nextId} was created.";
        }
    }
}
