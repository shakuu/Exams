using SchoolSystem.Framework.Models.Contracts;

namespace SchoolSystem.Framework.Core
{
    public interface ITeachersData
    {
        ISchoolSystemDataCollection<ITeacher> Teachers { get; }
    }
}
