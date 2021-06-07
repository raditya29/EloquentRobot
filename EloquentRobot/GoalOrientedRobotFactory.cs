using System;
using System.Collections.Generic;
using System.Linq;

namespace EloquentRobot
{
    public class GoalOrientedRobotFactory : IRobotFactory<GoalOrientedRobot>
    {
        public GoalOrientedRobot Create(string position, Parcel[] parcels) => 
            new GoalOrientedRobot(position, Array.Empty<string>(), parcels);
    }

}
