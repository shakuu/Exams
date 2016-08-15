namespace IntergalacticTravel.Tests.Mocks
{
    using System.Collections.Generic;

    using IntergalacticTravel;
    using IntergalacticTravel.Contracts;

    public class ExposedProtectedFieldsMockTeleportStation : TeleportStation
    {
        public ExposedProtectedFieldsMockTeleportStation(
            IBusinessOwner owner,
            IEnumerable<IPath> galacticMap,
            ILocation location)
            : base(owner, galacticMap, location)
        {
        }

        public IResources Resources
        {
            get
            {
                return base.resources;
            }
        }

        public IBusinessOwner Owner
        {
            get
            {
                return base.owner;
            }
        }

        public ILocation Location
        {
            get
            {
                return base.location;
            }
        }

        public IEnumerable<IPath> GalacticMap
        {
            get
            {
                return base.galacticMap;
            }
        }
    }
}
