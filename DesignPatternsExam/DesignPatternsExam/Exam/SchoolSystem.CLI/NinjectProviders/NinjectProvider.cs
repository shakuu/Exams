using System.Reflection;

using Ninject;

namespace SchoolSystem.Cli.NinjectProviders
{
    public class NinjectProvider
    {
        public static IKernel CreateNinject()
        {
            var ninject = new StandardKernel();
            ninject.Load(Assembly.GetExecutingAssembly());

            return ninject;
        }
    }
}
