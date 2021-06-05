using System.Collections.Generic;

namespace EloquentRobot
{
    public record FixedRouteRobot(Place Direction, Place[] Route) : Robot(Direction, Route)
    {
        private static readonly Place[] mailRoute = new Place[] {
            Village.ALICE_HOUSE, Village.CABIN, Village.ALICE_HOUSE, Village.BOB_HOUSE, Village.TOWN_HALL, Village.DARIA_HOUSE, 
            Village.ERNIE_HOUSE, Village.GRETE_HOUSE, Village.SHOP, Village.GRETE_HOUSE, Village.FARM, Village.MARKET_PLACE, Village.POST_OFFICE
        };

        public override Robot Move()
        {
            Queue<Place> route = new Queue<Place>(Route);
            return new FixedRouteRobot(route.Dequeue(), route.ToArray());
        }
    }
}
