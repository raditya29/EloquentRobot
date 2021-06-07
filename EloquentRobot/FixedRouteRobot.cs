using System;
using System.Collections.Generic;
using System.Linq;

namespace EloquentRobot
{
    public record FixedRouteRobot(string Position, Parcel[] Parcels, string[] Route) : Robot(Position, Parcels, Route)
    {
        private static readonly string[] mailRoute = new string[] {
            Village.ALICE_HOUSE, Village.CABIN, Village.ALICE_HOUSE, Village.BOB_HOUSE, Village.TOWN_HALL, Village.DARIA_HOUSE, 
            Village.ERNIE_HOUSE, Village.GRETE_HOUSE, Village.SHOP, Village.GRETE_HOUSE, Village.FARM, Village.MARKET_PLACE, Village.POST_OFFICE
        };

        public override Robot Move()
        {
            if (!HasUndeliveredParcels) throw new InvalidOperationException(nameof(UndeliveredParcels));

            Queue<string> route = (Route.Length == 0) ? new Queue<string>(mailRoute) : new Queue<string>(Route);
            string next = route.Dequeue();

            return new FixedRouteRobot(next,
                                       UndeliveredParcels.Select(parcel => parcel.Position == Position ? new Parcel(next, parcel.Destination) : parcel).ToArray(), // update pickup / delivery status
                                       route.ToArray()); 
        }
    }
}
