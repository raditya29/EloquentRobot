using System;
using System.Collections.Generic;
using System.Linq;

namespace EloquentRobot
{
    //public record GoalOrientedRobot(string Direction, Queue<string> Memory) : Robot(Direction, Memory);

    //public record LazyRobot(string Direction, Queue<string> Memory) : Robot(Direction, Memory);

    public abstract record Robot(Place Place, Place[] Route)
    {
        public Place Place { get; init; } = Place ?? throw new ArgumentNullException(nameof(Place));
        public Place[] Route { get; init; } = Route; // nullable
        public Parcel[]

        public abstract Robot Move();
    }

    public record GoalOrientedRobot(Place Place, Place[] Route) : Robot(Place, Route)
    {
        public override Robot Move()
        {
            if (Route.Length == 0)
            {
                var parcel = state.Parcels[0];
                if (parcel.Place != state.Place) route = new Queue<string>(FindRoute(Program.RoadGraph, state.Place, parcel.Place));
                else route = new Queue<string>(FindRoute(Program.RoadGraph, state.Place, parcel.Address));
            }

            return new GoalOrientedRobot(route.Dequeue(), route);
        }

        private static Place[] FindRoute(Place from, Place to)
        {
            var graph = Village.BuildGraph();
            var work = (new[] { new { At = from, Route = Array.Empty<Place>() } }).ToList();
            for (int i = 0; i < work.Count; i++)
            {
                var (at, route) = (work[i].At, work[i].Route);
                foreach (var place in graph[at])
                {
                    if (place == to) return route.Append(place).ToArray();
                    if (!work.Any(w => w.At == place)) work.Add(new { At = place, Route = route.Append(place).ToArray() });
                }
            }

            return Array.Empty<Place>(); // with our graph, we will never reach this code
        }
    }
}
