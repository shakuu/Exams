using System;
using System.Collections.Generic;

using SchoolSystem.Framework.Core.Commands.Contracts;

namespace SchoolSystem.Framework.Core.Commands
{
    public class RemoveTeacherCommand : ICommand
    {
        private readonly ITeachersData teachersData;

        public RemoveTeacherCommand(ITeachersData teachersData)
        {
            this.teachersData = teachersData;
        }

        public string Execute(IList<string> parameters, ISchoolSystemData schoolSystemData)
        {
            var teacherId = int.Parse(parameters[0]);

            if (!this.teachersData.Teachers.ContainsKey(teacherId))
            {
                throw new ArgumentException("The given key was not present in the dictionary.");
            }

            this.teachersData.Teachers.Remove(teacherId);
            return $"Teacher with ID {teacherId} was sucessfully removed.";
        }
    }
}
