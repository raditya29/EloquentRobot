using System;
using System.Linq;

namespace EloquentRobot
{
    public record DumbRobot(string Position, Parcel[] Parcels, string[] Route = null) : Robot(Position, Parcels, Route)
    {
        public override Robot Move()
        {
            if (!HasUndeliveredParcels) throw new InvalidOperationException(nameof(UndeliveredParcels));

            string[] possibleNextPlaces = Village.RoadGraph[Position];
            string next = possibleNextPlaces[new Random().Next(0, possibleNextPlaces.Length)];

            return new DumbRobot(next, 
                                 UndeliveredParcels.Select(parcel => parcel.Position == Position ? new Parcel(next, parcel.Destination) : parcel).ToArray(), // update pickup / delivery status
                                 null); 
        }
    }
}
