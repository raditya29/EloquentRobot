using System;
using System.Collections.Generic;
using System.Linq;

namespace EloquentRobot
{
    public class GoalOrientedRobotFactory : IRobotFactory<GoalOrientedRobot>
    {
        public GoalOrientedRobot Create(VillageState state, Queue<string> route)
        {
            if (route.Count == 0)
            {
                var parcel = state.Parcels[0];
                if (parcel.Place != state.Place) route = new Queue<string>(FindRoute(Program.RoadGraph, state.Place, parcel.Place));
                else route = new Queue<string>(FindRoute(Program.RoadGraph, state.Place, parcel.Address));
            }

            return new GoalOrientedRobot(route.Dequeue(), route);
        }

        private static string[] FindRoute(Dictionary<string, string[]> graph, string from, string to)
        {
            var work = (new[] { new { At = from, Route = Array.Empty<string>() } }).ToList();
            for (int i = 0; i < work.Count; i++)
            {
                var (at, route) = (work[i].At, work[i].Route);
                foreach (var place in graph[at])
                {
                    if (place == to) return route.Append(place).ToArray();
                    if (!work.Any(w => w.At == place)) work.Add(new { At = place, Route = route.Append(place).ToArray() });
                }
            }

            return Array.Empty<string>(); // with our graph, we will never reach this code
        }
    }

}
