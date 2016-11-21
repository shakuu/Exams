using System;

using SchoolSystem.Framework.Core.CommandProviders.Contracts;
using SchoolSystem.Framework.Core.Commands.Contracts;
using SchoolSystem.Framework.Core.Factories;

namespace SchoolSystem.Framework.Core.CommandProviders.Base
{
    public abstract class CommandProvider : ICommandProviderChainOfResponsibility
    {
        private readonly ICommandFactory commandFactory;

        private ICommandProvider nextElement;

        public CommandProvider(ICommandFactory commandFactory)
        {
            this.commandFactory = commandFactory;
        }

        protected ICommandFactory CommandFactory
        {
            get
            {
                return this.commandFactory;
            }
        }

        public ICommand ProvideCommand(string commandName)
        {
            if (this.CanProvideCommand(commandName))
            {
                return this.GetCommand();
            }
            else if (this.nextElement != null)
            {
                return this.nextElement.ProvideCommand(commandName);
            }
            else
            {
                throw new ArgumentException("The passed command is not found!");
            }
        }

        public void SetNextElement(ICommandProvider commandProvider)
        {
            this.nextElement = commandProvider;
        }

        protected abstract ICommand GetCommand();

        protected abstract bool CanProvideCommand(string commandName);
    }
}
