using SchoolSystem.Framework.Core.CommandProviders.Base;
using SchoolSystem.Framework.Core.Commands.Contracts;
using SchoolSystem.Framework.Core.Factories;

namespace SchoolSystem.Framework.Core.CommandProviders
{
    public class StudentListMarksProvider : CommandProvider
    {
        private const string Command = "StudentListMarks";

        public StudentListMarksProvider(ICommandFactory commandFactory)
            : base(commandFactory)
        {
        }

        protected override bool CanProvideCommand(string commandName)
        {
            return commandName == StudentListMarksProvider.Command;
        }

        protected override ICommand GetCommand()
        {
            return base.CommandFactory.GetStudentListMarksCommand();
        }
    }
}
