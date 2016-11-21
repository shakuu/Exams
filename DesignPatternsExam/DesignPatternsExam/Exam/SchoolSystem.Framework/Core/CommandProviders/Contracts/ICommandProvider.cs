using SchoolSystem.Framework.Core.Commands.Contracts;

namespace SchoolSystem.Framework.Core.CommandProviders.Contracts
{
    public interface ICommandProvider
    {
        ICommand ProvideCommand(string commandName);
    }
}
