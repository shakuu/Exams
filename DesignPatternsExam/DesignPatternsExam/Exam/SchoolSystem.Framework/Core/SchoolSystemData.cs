using System;

using SchoolSystem.Framework.Models.Contracts;

namespace SchoolSystem.Framework.Core
{
    public class ComposedSchoolSystemData : ISchoolSystemData
    {
        private readonly ISchoolSystemDataCollection<IStudent> students;
        private readonly ISchoolSystemDataCollection<ITeacher> teachers;

        public ComposedSchoolSystemData(ISchoolSystemDataCollection<IStudent> students, ISchoolSystemDataCollection<ITeacher> teachers)
        {
            if (students == null)
            {
                throw new ArgumentNullException(nameof(students));
            }

            if (teachers == null)
            {
                throw new ArgumentNullException(nameof(teachers));
            }

            this.students = students;
            this.teachers = teachers;
        }

        public ISchoolSystemDataCollection<IStudent> Students
        {
            get
            {
                return this.students;
            }
        }

        public ISchoolSystemDataCollection<ITeacher> Teachers
        {
            get
            {
                return this.teachers;
            }
        }
    }
}
