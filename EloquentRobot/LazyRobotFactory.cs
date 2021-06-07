using System;
using System.Linq;

namespace EloquentRobot
{
    public class LazyRobotFactory : IRobotFactory<LazyRobot>
    {
        public LazyRobot Create(string position, Parcel[] parcels) =>
            new LazyRobot(position, Array.Empty<string>(), parcels);
    }
}
