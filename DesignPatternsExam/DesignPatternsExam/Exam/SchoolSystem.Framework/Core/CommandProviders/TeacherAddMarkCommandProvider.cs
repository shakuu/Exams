using SchoolSystem.Framework.Core.CommandProviders.Base;
using SchoolSystem.Framework.Core.Commands.Contracts;
using SchoolSystem.Framework.Core.Factories;

namespace SchoolSystem.Framework.Core.CommandProviders
{
    public class TeacherAddMarkCommandProvider : CommandProvider
    {
        private const string Command = "TeacherAddMark";

        public TeacherAddMarkCommandProvider(ICommandFactory commandFactory)
            : base(commandFactory)
        {
        }

        protected override bool CanProvideCommand(string commandName)
        {
            return commandName == TeacherAddMarkCommandProvider.Command;
        }

        protected override ICommand GetCommand()
        {
            return base.CommandFactory.GetTeacherAddMarkCommand();
        }
    }
}
