using System.Collections.Generic;

namespace EloquentRobot
{
    public class FixedRouteRobotFactory : IRobotFactory<RouteRobot>
    {
        private static readonly string[] mailRoute = new string[] { // fixed route
            "Alice's House", "Cabin", "Alice's House", "Bob's House",
            "Town Hall", "Daria's House", "Ernie's House",
            "Grete's House", "Shop", "Grete's House", "Farm",
            "Marketplace", "Post Office"
        };

        public RouteRobot Create(VillageState state, Queue<string> memory)
        {
            if (memory.Count == 0) memory = new Queue<string>(mailRoute); // will be called exactly two times

            return new RouteRobot(memory.Dequeue(), memory);
        }
    }

}
