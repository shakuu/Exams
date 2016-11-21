using SchoolSystem.Framework.Core.CommandProviders.Base;
using SchoolSystem.Framework.Core.Commands.Contracts;
using SchoolSystem.Framework.Core.Factories;

namespace SchoolSystem.Framework.Core.CommandProviders
{
    public class CreateStudentCommandProvider : CommandProvider
    {
        private const string Command = "CreateStudent";

        public CreateStudentCommandProvider(ICommandFactory commandFactory) 
            : base(commandFactory)
        {
        }

        protected override bool CanProvideCommand(string commandName)
        {
            return commandName == CreateStudentCommandProvider.Command;
        }

        protected override ICommand GetCommand()
        {
            return base.CommandFactory.GetCreateStudentCommand();
        }
    }
}
