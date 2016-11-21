using SchoolSystem.Framework.Core.Commands.Contracts;

namespace SchoolSystem.Framework.Core.Factories
{
    public interface ICommandFactory
    {
        ICommand GetCreateStudentCommand();

        ICommand GetCreateTeacherCommand();

        ICommand GetRemoveStudentCommand();

        ICommand GetRemoveTeacherCommand();

        ICommand GetStudentListMarksCommand();

        ICommand GetTeacherAddMarkCommand();

        ICommand GetCommand(string commandName);
    }
}
