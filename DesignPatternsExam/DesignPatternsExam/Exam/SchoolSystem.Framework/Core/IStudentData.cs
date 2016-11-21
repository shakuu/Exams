using SchoolSystem.Framework.Models.Contracts;

namespace SchoolSystem.Framework.Core
{
    public interface IStudentData
    {
        ISchoolSystemDataCollection<IStudent> Students { get; }
    }
}
