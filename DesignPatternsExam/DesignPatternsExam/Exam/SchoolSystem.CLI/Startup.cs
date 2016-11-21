using Ninject;

using SchoolSystem.Framework.Core;
using SchoolSystem.Cli.NinjectProviders;

namespace SchoolSystem.Cli
{
    public class Startup
    {
        public static void Main()
        {
            var ninject = NinjectProvider.CreateNinject();
            var engine = ninject.Get<IEngine>();
            engine.Start();
        }
    }
}