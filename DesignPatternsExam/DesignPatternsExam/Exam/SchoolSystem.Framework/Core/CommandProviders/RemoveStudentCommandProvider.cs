using SchoolSystem.Framework.Core.CommandProviders.Base;
using SchoolSystem.Framework.Core.Commands.Contracts;
using SchoolSystem.Framework.Core.Factories;

namespace SchoolSystem.Framework.Core.CommandProviders
{
    public class RemoveStudentCommandProvider : CommandProvider
    {
        private const string Command = "RemoveStudent";

        public RemoveStudentCommandProvider(ICommandFactory commandFactory) 
            : base(commandFactory)
        {
        }

        protected override bool CanProvideCommand(string commandName)
        {
            return commandName == RemoveStudentCommandProvider.Command;
        }

        protected override ICommand GetCommand()
        {
            return base.CommandFactory.GetRemoveStudentCommand();
        }
    }
}
