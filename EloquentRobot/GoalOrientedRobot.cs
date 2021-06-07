using System;
using System.Collections.Generic;
using System.Linq;

namespace EloquentRobot
{
    public record GoalOrientedRobot(string Position, Parcel[] Parcels, string[] Route) : Robot(Position, Parcels, Route) // start with empty route
    {
        public override Robot Move()
        {
            if (!HasUndeliveredParcels) throw new InvalidOperationException(nameof(UndeliveredParcels));

            var route = new Queue<string>(Route);
            if (Route.Length == 0)
            {
                var parcel = UndeliveredParcels.First();
                if (parcel.Position != Position) route = new Queue<string>(FindRoute(Position, parcel.Position)); // not yet picked up
                else route = new Queue<string>(FindRoute(Position, parcel.Destination)); // picked up, but not yet delivered
            }

            string next = route.Dequeue();
            return new GoalOrientedRobot(next, 
                                         UndeliveredParcels.Select(parcel => parcel.Position == Position ? new Parcel(next, parcel.Destination) : parcel).ToArray(), // update pickup / delivery status
                                         route.ToArray()); 
        }
    }
}
