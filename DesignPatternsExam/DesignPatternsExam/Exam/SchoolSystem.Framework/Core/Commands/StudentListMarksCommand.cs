using System.Collections.Generic;
using SchoolSystem.Framework.Core.Commands.Contracts;

namespace SchoolSystem.Framework.Core.Commands
{
    public class StudentListMarksCommand : ICommand
    {
        private readonly IStudentData studentData;

        public StudentListMarksCommand(IStudentData studentData)
        {
            this.studentData = studentData;
        }

        public string Execute(IList<string> parameters, ISchoolSystemData schoolSystemData)
        {
            var studentId = int.Parse(parameters[0]);
            var student = this.studentData.Students.GetById(studentId);
            return student.ListMarks();
        }
    }
}
