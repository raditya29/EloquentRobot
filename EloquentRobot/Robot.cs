﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace EloquentRobot
{
    public abstract record Robot(string Position, Parcel[] Parcels, string[] Route)
    {
        public string Position { get; init; } = Position ?? throw new ArgumentNullException(nameof(Position));
        public Parcel[] Parcels { get; init; } = Parcels ?? throw new ArgumentNullException(nameof(Parcels));
        public string[] Route { get; init; } = Route ?? throw new ArgumentNullException(nameof(Route));

        protected IEnumerable<Parcel> UndeliveredParcels => Parcels.Where(parcel => !(parcel.Position == Position && parcel.Destination == Position));

        public bool HasUndeliveredParcels => UndeliveredParcels.Any();

        public abstract Robot Move();

        public static string[] FindRoute(string from, string to)
        {
            var graph = Village.BuildGraph();
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
