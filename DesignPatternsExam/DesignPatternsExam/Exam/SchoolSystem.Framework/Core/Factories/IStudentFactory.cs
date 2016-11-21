using SchoolSystem.Framework.Models;
using SchoolSystem.Framework.Models.Enums;

namespace SchoolSystem.Framework.Core.Factories
{
    public interface IStudentFactory
    {
        Student CreateStudent(string firstName, string lastName, Grade grade);
    }
}
