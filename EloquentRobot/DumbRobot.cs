using System;
using System.Linq;

namespace EloquentRobot
{
    public record DumbRobot(string Position, string[] Route, Parcel[] Parcels) : Robot(Position, Route, Parcels)
    {
        public override Robot Move()
        {
            if (Parcels.Length == 0) throw new InvalidOperationException(nameof(Parcels));

            string[] possibleNextPlaces = Village.RoadGraph[Position];
            string next = possibleNextPlaces[new Random().Next(0, possibleNextPlaces.Length)];

            return new DumbRobot(next, null,
                                 Parcels.Select(parcel => parcel.Position != Position ? parcel : new Parcel(next, parcel.Destination)) // update pickup / delivery status
                                        .Where(parcel => parcel.Position != parcel.Destination).ToArray()); // drop if destination
        }
    }
}
