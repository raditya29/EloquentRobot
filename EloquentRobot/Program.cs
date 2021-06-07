using System;
using System.Linq;

namespace EloquentRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunRobot(new DumbRobot(Village.POST_OFFICE, Parcel.ProduceRandomParcels(), Array.Empty<string>()));
            //RunRobot(new FixedRouteRobot(Village.POST_OFFICE, Parcel.ProduceRandomParcels(), Array.Empty<string>()));
            //RunRobot(new GoalOrientedRobot(Village.POST_OFFICE, Parcel.ProduceRandomParcels(), Array.Empty<string>()));
            RunRobot(new LazyRobot(Village.POST_OFFICE, Parcel.ProduceRandomParcels(), Array.Empty<string>()));

            //CompareRobots(new DumbRobotFactory(), new FixedRouteRobotFactory());
            //CompareRobots(new FixedRouteRobotFactory(), new GoalOrientedRobotFactory());
            //CompareRobots(new GoalOrientedRobotFactory(), new LazyRobotFactory());
        }

        static int CountSteps(Robot robot)
        {
            int steps = 0;
            while (robot.HasUndeliveredParcels)
            {
                robot = robot.Move();
                steps++;
            }

            return steps;
        }

        static void RunRobot(Robot robot)
        {
            int steps = 0;
            while (robot.HasUndeliveredParcels)
            {
                robot = robot.Move();
                Console.WriteLine($"at {robot.Position}. Dropping {robot.Parcels.Count(parcel => parcel.Position == robot.Position && parcel.Destination == robot.Position)} parcel(s).");
                steps++;
            }

            Console.WriteLine($"Done in {steps}.");
        }

        static void CompareRobots(IRobotFactory<Robot> robot1Factory, IRobotFactory<Robot> robot2Factory) 
        {
            int total1 = 0, total2 = 0;
            var robot1FactoryType = string.Empty;
            var robot2FactoryType = string.Empty;
            for (int i = 0; i < 100; i++)
            {
                var parcels = Parcel.ProduceRandomParcels();
                var robot1 = robot1Factory.Create(Village.POST_OFFICE, parcels);
                if (robot1FactoryType == string.Empty) robot1FactoryType = robot1.GetType().Name;
                total1 += CountSteps(robot1);

                var robot2 = robot2Factory.Create(Village.POST_OFFICE, parcels);
                if (robot2FactoryType == string.Empty) robot2FactoryType = robot2.GetType().Name;
                total2 += CountSteps(robot2);
            }

            Console.WriteLine($"{robot1FactoryType} averaging {(decimal)total1 / 100} steps per task.");
            Console.WriteLine($"{robot2FactoryType} averaging {(decimal)total2 / 100} steps per task.");
        }
    }
}
