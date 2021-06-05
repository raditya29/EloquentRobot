using System;
using System.Collections.Generic;
using System.Linq;

namespace EloquentRobot
{
    public class LazyRobotFactory : IRobotFactory<LazyRobot>
    {
        public LazyRobot Create(Village state, Queue<string> route)
        {
            var (place, parcels) = (state.Place, state.Parcels);
            if (route.Count == 0)
            {
                // Describe a route for every parcel
                var routes = parcels.Select(parcel => {
                    if (parcel.Place != place) return new RoutePickupPair(FindRoute(Program.RoadGraph, place, parcel.Place), true);
                    else return new RoutePickupPair(FindRoute(Program.RoadGraph, place, parcel.Address), false);
                }).ToArray();

                // This determines the precedence a route gets when choosing.
                // Route length counts negatively, routes that pick up a package
                // get a small bonus.
                route = new Queue<string>(routes.Aggregate(routes[0], 
                                            (accumulator, current) => 
                                                (score(accumulator.Route, accumulator.Pickup) > score(current.Route, current.Pickup) ? accumulator : current)).Route);
            }

            return new LazyRobot(route.Dequeue(), route);
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

        private static decimal score(string[] route, bool pickup) => (decimal)((pickup ? 0.5 : 0) - route.Length);

        private record RoutePickupPair(string[] Route, bool Pickup);
    }
}
