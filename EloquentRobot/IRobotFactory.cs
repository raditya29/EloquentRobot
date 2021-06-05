using System.Collections.Generic;

namespace EloquentRobot
{
    public interface IRobotFactory<out TRobot> where TRobot : Robot
    {
        TRobot Create(Village state, Queue<string> memory);
    }

    public class DumbRobotFactory : IRobotFactory<Robot>
    {
        public Robot Create(Village state, Queue<string> Memory) => new Robot(Program.RandomPick(Program.RoadGraph[state.Place]));
    }
}
