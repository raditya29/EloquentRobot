using System;

namespace EloquentRobot
{
    public class DumbRobotFactory : IRobotFactory<DumbRobot>
    {
        public DumbRobot Create(string position, Parcel[] parcels) => new DumbRobot(position, Array.Empty<string>(), parcels);
    }
}
