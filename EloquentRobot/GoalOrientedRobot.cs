using System;
using System.Collections.Generic;
using System.Linq;

namespace EloquentRobot
{
    public record GoalOrientedRobot(string Position, string[] Route, Parcel[] Parcels) : Robot(Position, Route, Parcels) // start with empty route
    {
        public override Robot Move()
        {
            if (Parcels.Length == 0) throw new InvalidOperationException(nameof(Parcels));

            var route = new Queue<string>(Route);
            if (Route.Length == 0)
            {
                var parcel = Parcels[0];
                if (parcel.Position != Position) route = new Queue<string>(FindRoute(Position, parcel.Position));
                else route = new Queue<string>(FindRoute(Position, parcel.Destination));
            }

            string next = route.Dequeue();
            var notDroppedParcels = Parcels.Where(parcel => !(parcel.Destination == Position));
            return new GoalOrientedRobot(next, route.ToArray(),
                                         Parcels.Select(parcel => parcel.Position != Position ? parcel : new Parcel(next, parcel.Destination)) // update pickup / delivery status
                                                .Where(parcel => parcel.Position != parcel.Destination).ToArray()); // drop if destination
        }
    }
}
