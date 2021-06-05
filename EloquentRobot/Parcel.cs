using System;
using System.Collections.Generic;
using System.Linq;

namespace EloquentRobot
{
    public class Parcel
    {
        public Place Origin { get; init; }
        public Place Destination { get; init; }
        public ParcelStatus State { get; init; }

        public Parcel(Place origin, Place destination, ParcelStatus state)
        {
            Origin = origin ?? throw new ArgumentNullException(nameof(origin));
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
            State = state;
        }

        static Parcel[] ProduceRandomParcels(int parcelCounts = 5)
        {
            var parcels = new Stack<Parcel>();
            for (int index = 0; index < parcelCounts; index++)
            {
                var random = new Random();
                var places = Program.RoadGraph.Keys.ToArray();
                var origin = places[random.Next(0, places.Length)];
                Place destination;
                do
                {
                    destination = places[random.Next(0, places.Length)];
                } while (origin == destination);

                parcels.Push(new Parcel(origin, destination, ParcelStatus.NotPickup));
            }

            return parcels.ToArray();
        }
    }

    public enum ParcelStatus
    {
        NotPickup = 0,
        OnDelivery,
        Delivered
    }
}
