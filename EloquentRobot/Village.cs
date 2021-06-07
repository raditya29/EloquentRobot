using System.Collections.Generic;
using System.Linq;

namespace EloquentRobot
{
    public class Village
    {
        public static readonly string ALICE_HOUSE = "Alice's House";
        public static readonly string BOB_HOUSE = "Bob's House";
        public static readonly string CABIN = "Cabin";
        public static readonly string DARIA_HOUSE = "Daria's House";
        public static readonly string ERNIE_HOUSE = "Ernie's House";
        public static readonly string GRETE_HOUSE = "Grete's House";
        public static readonly string FARM = "Farm";
        public static readonly string MARKET_PLACE = "Marketplace";
        public static readonly string POST_OFFICE = "Post Office";
        public static readonly string SHOP = "Shop";
        public static readonly string TOWN_HALL = "Town Hall";

        public static readonly Road[] Roads = new Road[] {
            new(ALICE_HOUSE, BOB_HOUSE), new(ALICE_HOUSE, CABIN), new(ALICE_HOUSE, POST_OFFICE), new(BOB_HOUSE, TOWN_HALL),
            new(DARIA_HOUSE, ERNIE_HOUSE), new(DARIA_HOUSE, TOWN_HALL), new(ERNIE_HOUSE, GRETE_HOUSE), new(GRETE_HOUSE, FARM),
            new(GRETE_HOUSE, SHOP), new(MARKET_PLACE, FARM), new(MARKET_PLACE, POST_OFFICE), new(MARKET_PLACE, SHOP),
            new(MARKET_PLACE, TOWN_HALL), new(SHOP, TOWN_HALL)
        };

        public static Dictionary<string, string[]> RoadGraph { get; } = BuildGraph();

        public static Dictionary<string, string[]> BuildGraph()
        {
            var graph = new Dictionary<string, Queue<string>>();

            foreach (var (place1, place2) in Roads)
            {
                if (graph.ContainsKey(place1)) graph[place1].Enqueue(place2);
                else graph.Add(place1, new Queue<string>(new string[] { place2 }));

                if (graph.ContainsKey(place2)) graph[place2].Enqueue(place1);
                else graph.Add(place2, new Queue<string>(new string[] { place1 }));
            }

            return graph.ToDictionary(g => g.Key, g => g.Value.ToArray());
        }
    }

}
