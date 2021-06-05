using System;
using System.Collections.Generic;
using System.Linq;

namespace EloquentRobot
{
    class Program
    {
        

        

        internal static readonly Dictionary<Place, Place[]> RoadGraph = BuildGraph(roads);

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

        static Dictionary<Place, Place[]> BuildGraph(Road[] roads)
        {
            var graph = new Dictionary<Place, Stack<Place>>();

            foreach (var (place1, place2) in roads)
            {
                if (graph.ContainsKey(place1)) graph[place1].Push(place2);
                else graph.Add(place1, new Stack<Place>(new Place[] { place2 }));

                if (graph.ContainsKey(place2)) graph[place2].Push(place1);
                else graph.Add(place2, new Stack<Place>(new Place[] { place1 }));
            }

            return graph.ToDictionary(g => g.Key, g => g.Value.ToArray());
        }

        static int CountSteps(Village state,
            Func<Village, Queue<string>, Robot> robotFactory, Queue<string> memory)
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

        static void RunRobot(Village state,
            Func<Village, Queue<string>, Robot> robotFactory, Queue<string> memory) // memory route robot factory
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

        static void CompareRobots(Func<Village, Queue<string>, Robot> robot1Factory, Queue<string> robot1Memory,
            Func<Village, Queue<string>, Robot> robot2Factory, Queue<string> robot2Memory) 
        {
            int total1 = 0, total2 = 0;
            for (int i = 0; i < 100; i++)
            {
                var state = Village.Random();
                total1 += CountSteps(state, robot1Factory, robot1Memory);
                total2 += CountSteps(state, robot2Factory, robot2Memory);
            }

            Console.WriteLine($"first robot averaging {(decimal)total1 / 100} steps per task.");
            Console.WriteLine($"second robot averaging {(decimal)total2 / 100} steps per task.");
        }

        internal static string RandomPick(string[] places) => places[new Random().Next(0, places.Length)];
    }

    public record Place(string Name);

    public record Road(Place PlaceA, Place PlaceB);
}
