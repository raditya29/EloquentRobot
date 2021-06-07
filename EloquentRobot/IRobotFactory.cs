using System.Collections.Generic;

namespace EloquentRobot
{
    public interface IRobotFactory<out TRobot> where TRobot : Robot
    {
        TRobot Create(string position, Parcel[] parcels);
    }
}
