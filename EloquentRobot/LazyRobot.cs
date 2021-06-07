using System;
using System.Collections.Generic;
using System.Linq;

namespace EloquentRobot
{
    public record LazyRobot(string Position, Parcel[] Parcels, string[] Route) : Robot(Position, Parcels, Route)
    {
        public override Robot Move()
        {
            if (!HasUndeliveredParcels) throw new InvalidOperationException(nameof(UndeliveredParcels));

            Queue<string> route = new Queue<string>(Route);
            if (Route.Length == 0)
            {
                // Describe a route for every parcel
                var routes = UndeliveredParcels.Select(parcel =>
                {
                    if (parcel.Position != Position) return new RoutePickupPair(FindRoute(Position, parcel.Position), true);
                    else return new RoutePickupPair(FindRoute(Position, parcel.Destination), false);
                });

                // This determines the precedence a route gets when choosing.
                // Route length counts negatively, routes that pick up a package
                // get a small bonus.
                var filteredRoute = routes.Aggregate(routes.First(),
                                            (accumulator, current) =>
                                                (Score(accumulator.Route, accumulator.Pickup) > Score(current.Route, current.Pickup) ? accumulator : current)).Route;
                route = new Queue<string>(filteredRoute);
            }
            
            string next = route.Dequeue();
            return new LazyRobot(next,
                                 UndeliveredParcels.Select(parcel => parcel.Position == Position ? new Parcel(next, parcel.Destination) : parcel).ToArray(), // update pickup / delivery status
                                 route.ToArray()); 
        }

        private static decimal Score(string[] route, bool pickup) => (decimal)((pickup ? 0.5 : 0) - route.Length);

        private record RoutePickupPair(string[] Route, bool Pickup);
    }
}
