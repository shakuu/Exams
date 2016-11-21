using SchoolSystem.Framework.Core.CommandProviders.Base;
using SchoolSystem.Framework.Core.Commands.Contracts;
using SchoolSystem.Framework.Core.Factories;

namespace SchoolSystem.Framework.Core.CommandProviders
{
    public class RemoveTeacherCommandProvider : CommandProvider
    {
        private const string Command = "RemoveTeacher";

        public RemoveTeacherCommandProvider(ICommandFactory commandFactory) 
            : base(commandFactory)
        {
        }

        protected override bool CanProvideCommand(string commandName)
        {
            return commandName == RemoveTeacherCommandProvider.Command;
        }

        protected override ICommand GetCommand()
        {
            return base.CommandFactory.GetRemoveTeacherCommand();
        }
    }
}
