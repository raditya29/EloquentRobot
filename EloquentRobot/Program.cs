using System;
using System.Collections.Generic;
using System.Linq;

namespace EloquentRobot
{
    class Program
    {
        static readonly string[] roads = new string[] {
            "Alice's House-Bob's House", "Alice's House-Cabin",
            "Alice's House-Post Office", "Bob's House-Town Hall",
            "Daria's House-Ernie's House", "Daria's House-Town Hall",
            "Ernie's House-Grete's House", "Grete's House-Farm",
            "Grete's House-Shop", "Marketplace-Farm",
            "Marketplace-Post Office", "Marketplace-Shop",
            "Marketplace-Town Hall", "Shop-Town Hall"
        };

        internal static readonly Dictionary<string, string[]> RoadGraph = BuildGraph(roads);

        static void Main(string[] args)
        {
            //var sharedRandomVillageState = VillageState.Random();
            //RunRobot(VillageState.Random(), new DumbRobotFactory().Create, null); // run dumb robot
            //RunRobot(sharedRandomVillageState, new FixedRouteRobotFactory().Create, new Queue<string>()); // run route robot
            //RunRobot(sharedRandomVillageState, new GoalOrientedRobotFactory().Create, new Queue<string>()); // run goal oriented robot

            CompareRobots(new DumbRobotFactory().Create, null, new FixedRouteRobotFactory().Create, new Queue<string>());
            //CompareRobots(new FixedRouteRobotFactory().Create, new Queue<string>(), new GoalOrientedRobotFactory().Create, new Queue<string>());
            //CompareRobots(new GoalOrientedRobotFactory().Create, new Queue<string>(), new LazyRobotFactory().Create, new Queue<string>());
        }

        static Dictionary<string, string[]> BuildGraph(string[] roads)
        {
            var tempGraph = new Dictionary<string, Stack<string>>();

            foreach (string[] fromTo in roads.Select(road => road.Split('-')))
            {
                if (tempGraph.ContainsKey(fromTo[0])) tempGraph[fromTo[0]].Push(fromTo[1]);
                else tempGraph.Add(fromTo[0], new Stack<string>(new string[] { fromTo[1] }));

                if (tempGraph.ContainsKey(fromTo[1])) tempGraph[fromTo[1]].Push(fromTo[0]);
                else tempGraph.Add(fromTo[1], new Stack<string>(new string[] { fromTo[0] }));
            }

            var graph = new Dictionary<string, string[]>();
            foreach (var pair in tempGraph) graph.Add(pair.Key, pair.Value.ToArray());

            return graph;
        }

        static int CountSteps(VillageState state,
            Func<VillageState, Queue<string>, Robot> robotFactory, Queue<string> memory)
        {
            int turn = 0;
            while (true)
            {
                if (state.Parcels.Length == 0) break;

                var action = robotFactory(state, memory);
                state = state.Move(action.Direction);
                memory = action.Memory;

                turn++;
            }

            return turn;
        }

        static void RunRobot(VillageState state,
            Func<VillageState, Queue<string>, Robot> robotFactory, Queue<string> memory) // memory route robot factory
        {
            int turn = 0;
            while (true)
            {
                if (state.Parcels.Length == 0)
                {
                    Console.WriteLine($"Done in {turn}");
                    break;
                }

                var action = robotFactory(state, memory);
                state = state.Move(action.Direction);
                memory = action.Memory;
                Console.WriteLine($"Moved to {action.Direction}");
                turn++;
            }
        }

        static void CompareRobots(Func<VillageState, Queue<string>, Robot> robot1Factory, Queue<string> robot1Memory,
            Func<VillageState, Queue<string>, Robot> robot2Factory, Queue<string> robot2Memory) 
        {
            int total1 = 0, total2 = 0;
            for (int i = 0; i < 100; i++)
            {
                var state = VillageState.Random();
                total1 += CountSteps(state, robot1Factory, robot1Memory);
                total2 += CountSteps(state, robot2Factory, robot2Memory);
            }

            Console.WriteLine($"first robot averaging {(decimal)total1 / 100} steps per task.");
            Console.WriteLine($"second robot averaging {(decimal)total2 / 100} steps per task.");
        }

        internal static string RandomPick(string[] places) => places[new Random().Next(0, places.Length)];
    }

    public record Parcel(string Place, string Address);
}
