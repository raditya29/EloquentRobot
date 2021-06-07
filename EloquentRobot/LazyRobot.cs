using System;
using System.Linq;

namespace EloquentRobot
{
    public record LazyRobot(string Position, string[] Route, Parcel[] Parcels) : Robot(Position, Route, Parcels)
    {
        public override Robot Move()
        {
            string[] route = Array.Empty<string>();
            if (Route.Length == 0)
            {
                // Describe a route for every parcel
                var routes = Parcels.Select(parcel =>
                {
                    if (parcel.Position != Position) return new RoutePickupPair(FindRoute(Position, parcel.Position), true);
                    else return new RoutePickupPair(FindRoute(Position, parcel.Destination), false);
                }).ToArray();

                // This determines the precedence a route gets when choosing.
                // Route length counts negatively, routes that pick up a package
                // get a small bonus.
                route = routes.Aggregate(routes[0],
                                            (accumulator, current) =>
                                                (Score(accumulator.Route, accumulator.Pickup) > Score(current.Route, current.Pickup) ? accumulator : current)).Route;
            }

            return new LazyRobot(Position, route, Parcels);
        }

        private static decimal Score(string[] route, bool pickup) => (decimal)((pickup ? 0.5 : 0) - route.Length);

        private record RoutePickupPair(string[] Route, bool Pickup);
    }
}
