using SchoolSystem.Framework.Models;
using SchoolSystem.Framework.Models.Enums;

namespace SchoolSystem.Framework.Core.Factories
{
    public interface IMarkFactory
    {
        Mark CreateMark(Subject subject, float value);
    }
}
