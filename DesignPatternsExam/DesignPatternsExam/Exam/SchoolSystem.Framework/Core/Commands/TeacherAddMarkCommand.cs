using System.Collections.Generic;

using SchoolSystem.Framework.Core.Commands.Contracts;

namespace SchoolSystem.Framework.Core.Commands
{
    public class TeacherAddMarkCommand : ICommand
    {
        private readonly IStudentData studentData;
        private readonly ITeachersData teachersData;

        public TeacherAddMarkCommand(IStudentData studentData, ITeachersData teachersData)
        {
            this.studentData = studentData;
            this.teachersData = teachersData;
        }

        public string Execute(IList<string> parameters, ISchoolSystemData schoolSystemData)
        {
            var teacherId = int.Parse(parameters[0]);
            var studentId = int.Parse(parameters[1]);
            var mark = float.Parse(parameters[2]);

            var student = this.studentData.Students.GetById(studentId);
            var teacher = this.teachersData.Teachers.GetById(teacherId);

            teacher.AddMark(student, mark);
            return $"Teacher {teacher.FirstName} {teacher.LastName} added mark {mark} to student {student.FirstName} {student.LastName} in {teacher.Subject}.";
        }
    }
}
