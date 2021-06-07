using System;
using System.Collections.Generic;
using System.Linq;

namespace EloquentRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunRobot(new DumbRobot(Village.POST_OFFICE, null, Parcel.ProduceRandomParcels()));
            //RunRobot(new FixedRouteRobot(Village.POST_OFFICE, Array.Empty<string>(), Parcel.ProduceRandomParcels()));
            RunRobot(new GoalOrientedRobot(Village.POST_OFFICE, Array.Empty<string>(), Parcel.ProduceRandomParcels()));

            // var sharedRandomVillageState = VillageState.Random();
            // RunRobot(new DumbRobotFactory().Create(Village.POST_OFFICE, Parcel.ProduceRandomParcels())); // run dumb robot
            // RunRobot(new FixedRouteRobotFactory().Create(Village.POST_OFFICE, Parcel.ProduceRandomParcels())); // run route robot
            // RunRobot(sharedRandomVillageState, new GoalOrientedRobotFactory().Create, new Queue<string>()); // run goal oriented robot

            // CompareRobots(new DumbRobotFactory().Create, new FixedRouteRobotFactory().Create);
            //CompareRobots(new FixedRouteRobotFactory().Create, new Queue<string>(), new GoalOrientedRobotFactory().Create, new Queue<string>());
            //CompareRobots(new GoalOrientedRobotFactory().Create, new Queue<string>(), new LazyRobotFactory().Create, new Queue<string>());
        }

        static int CountSteps(Robot robot)
        {
            int steps = 0;
            while (robot.Parcels.Length != 0)
            {
                robot = robot.Move();
                steps++;
            }

            return steps;
        }

        static void RunRobot(Robot robot)
        {
            int steps = 0;
            while (robot.Parcels.Length != 0)
            {
                robot = robot.Move();
                Console.WriteLine($"Moved to {robot.Position}");
                steps++;
            }

            Console.WriteLine($"Done in {steps}.");
        }

        static void CompareRobots(Func<string, Parcel[], Robot> robot1Factory, Func<string, Parcel[], Robot> robot2Factory) 
        {
            int total1 = 0, total2 = 0;
            for (int i = 0; i < 100; i++)
            {
                var parcels = Parcel.ProduceRandomParcels();
                var robot1 = robot1Factory(Village.POST_OFFICE, parcels);
                var robot2 = robot2Factory(Village.POST_OFFICE, parcels);
                total1 += CountSteps(robot1);
                total2 += CountSteps(robot2);
            }

            Console.WriteLine($"first robot averaging {(decimal)total1 / 100} steps per task.");
            Console.WriteLine($"second robot averaging {(decimal)total2 / 100} steps per task.");
        }

        internal static string RandomPick(string[] places) => places[new Random().Next(0, places.Length)];
    }

    public record Road(string PlaceA, string PlaceB);
}
