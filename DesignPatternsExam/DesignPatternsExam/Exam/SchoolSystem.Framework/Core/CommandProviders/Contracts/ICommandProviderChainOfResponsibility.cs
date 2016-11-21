namespace SchoolSystem.Framework.Core.CommandProviders.Contracts
{
    public interface ICommandProviderChainOfResponsibility : ICommandProvider
    {
        void SetNextElement(ICommandProvider commandProvider);
    }
}
