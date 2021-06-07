using System;

namespace EloquentRobot
{
    public class FixedRouteRobotFactory : IRobotFactory<FixedRouteRobot>
    {
        public FixedRouteRobot Create(string position, Parcel[] parcels) => new FixedRouteRobot(position, Array.Empty<string>(), parcels);
    }

}
