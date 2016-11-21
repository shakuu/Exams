using System;
using System.Collections.Generic;

using SchoolSystem.Framework.Core.Commands.Contracts;

namespace SchoolSystem.Framework.Core.Commands
{
    public class RemoveStudentCommand : ICommand
    {
        private readonly IStudentData studentData;

        public RemoveStudentCommand(IStudentData studentData)
        {
            this.studentData = studentData;
        }

        public string Execute(IList<string> parameters, ISchoolSystemData schoolSystemData)
        {
            var studentId = int.Parse(parameters[0]);
            if (!this.studentData.Students.ContainsKey(studentId))
            {
                throw new ArgumentException("The given key was not present in the dictionary.");
            }

            this.studentData.Students.Remove(studentId);
            return $"Student with ID {studentId} was sucessfully removed.";
        }
    }
}
