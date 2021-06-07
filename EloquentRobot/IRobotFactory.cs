using System;

namespace EloquentRobot
{
    public interface IRobotFactory<out TRobot> where TRobot : Robot
    {
        TRobot Create(string position, Parcel[] parcels);
    }

    public class DumbRobotFactory : IRobotFactory<DumbRobot>
    {
        public DumbRobot Create(string position, Parcel[] parcels) => new DumbRobot(position, Array.Empty<string>(), parcels);
    }

    public class FixedRouteRobotFactory : IRobotFactory<FixedRouteRobot>
    {
        public FixedRouteRobot Create(string position, Parcel[] parcels) => new FixedRouteRobot(position, Array.Empty<string>(), parcels);
    }

    public class GoalOrientedRobotFactory : IRobotFactory<GoalOrientedRobot>
    {
        public GoalOrientedRobot Create(string position, Parcel[] parcels) =>
            new GoalOrientedRobot(position, Array.Empty<string>(), parcels);
    }

    public class LazyRobotFactory : IRobotFactory<LazyRobot>
    {
        public LazyRobot Create(string position, Parcel[] parcels) =>
            new LazyRobot(position, Array.Empty<string>(), parcels);
    }
}
