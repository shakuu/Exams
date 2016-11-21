using SchoolSystem.Framework.Core.CommandProviders.Base;
using SchoolSystem.Framework.Core.Commands.Contracts;
using SchoolSystem.Framework.Core.Factories;

namespace SchoolSystem.Framework.Core.CommandProviders
{
    public class CreateTeacherCommandProvider : CommandProvider
    {
        private const string Command = "CreateTeacher";

        public CreateTeacherCommandProvider(ICommandFactory commandFactory)
            : base(commandFactory)
        {
        }

        protected override bool CanProvideCommand(string commandName)
        {
            return commandName == CreateTeacherCommandProvider.Command;
        }

        protected override ICommand GetCommand()
        {
            return base.CommandFactory.GetCreateTeacherCommand();
        }
    }
}
