using SchoolSystem.Framework.Core.Contracts;

namespace SchoolSystem.Framework.Core.Providers
{
    public class SchoolSystemIdentityProvider : IIdentityProvider
    {
        private int lastId = 0;

        public int NextId()
        {
            return lastId++;
        }
    }
}
