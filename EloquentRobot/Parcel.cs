using System;
using System.Collections.Generic;
using System.Linq;

namespace EloquentRobot
{
    public class Parcel
    {
        public string Position { get; init; }
        public string Destination { get; init; }

        public Parcel(string position, string destination)
        {
            Position = position ?? throw new ArgumentNullException(nameof(position));
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
        }

        public static Parcel[] ProduceRandomParcels(int parcelCounts = 5)
        {
            var parcels = new Stack<Parcel>();
            for (int index = 0; index < parcelCounts; index++)
            {
                var random = new Random();
                var places = Village.RoadGraph.Keys.ToArray();
                var origin = places[random.Next(0, places.Length)];
                string destination;
                do
                {
                    destination = places[random.Next(0, places.Length)];
                } while (origin == destination);

                parcels.Push(new Parcel(origin, destination));
            }

            return parcels.ToArray();
        }
    }
}
