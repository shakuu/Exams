using SchoolSystem.Framework.Models;
using SchoolSystem.Framework.Models.Enums;

namespace SchoolSystem.Framework.Core.Factories
{
    public  interface ITeacherFactory
    {
        Teacher CreateTeacher(string firstName, string lastName, Subject subject);
    }
}
