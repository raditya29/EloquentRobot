using System;
using System.Collections.Generic;
using System.Linq;

namespace EloquentRobot
{
    public class Village
    {
        public static readonly Place ALICE_HOUSE = new Place("Alice's House");
        public static readonly Place BOB_HOUSE = new Place("Bob's House");
        public static readonly Place CABIN = new Place("Cabin");
        public static readonly Place DARIA_HOUSE = new Place("Daria's House");
        public static readonly Place ERNIE_HOUSE = new Place("Ernie's House");
        public static readonly Place GRETE_HOUSE = new Place("Grete's House");
        public static readonly Place FARM = new Place("Farm");
        public static readonly Place MARKET_PLACE = new Place("Marketplace");
        public static readonly Place POST_OFFICE = new Place("Post Office");
        public static readonly Place SHOP = new Place("Shop");
        public static readonly Place TOWN_HALL = new Place("Town Hall");

        public static readonly Road[] Roads = new Road[] {
            new(ALICE_HOUSE, BOB_HOUSE), new(ALICE_HOUSE, CABIN), new(ALICE_HOUSE, POST_OFFICE), new(BOB_HOUSE, TOWN_HALL),
            new(DARIA_HOUSE, ERNIE_HOUSE), new(DARIA_HOUSE, TOWN_HALL), new(ERNIE_HOUSE, GRETE_HOUSE), new(GRETE_HOUSE, FARM),
            new(GRETE_HOUSE, SHOP), new(MARKET_PLACE, FARM), new(MARKET_PLACE, POST_OFFICE), new(MARKET_PLACE, SHOP),
            new(MARKET_PLACE, TOWN_HALL), new(SHOP, TOWN_HALL)
        };

        public static Dictionary<Place, Place[]> BuildGraph()
        {
            var graph = new Dictionary<Place, Stack<Place>>();

            foreach (var (place1, place2) in Roads)
            {
                if (graph.ContainsKey(place1)) graph[place1].Push(place2);
                else graph.Add(place1, new Stack<Place>(new Place[] { place2 }));

                if (graph.ContainsKey(place2)) graph[place2].Push(place1);
                else graph.Add(place2, new Stack<Place>(new Place[] { place1 }));
            }

            return graph.ToDictionary(g => g.Key, g => g.Value.ToArray());
        }
    }

}
