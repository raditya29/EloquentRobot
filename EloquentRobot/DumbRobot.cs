using System;

namespace EloquentRobot
{
    public record DumbRobot(Place Place, Place[] Route) : Robot(Place, Route)
    {
        public override Robot Move()
        {
            Place[] possibleNextPlaces = Program.RoadGraph[Place];
            Random random = new();
            Place next = possibleNextPlaces[random.Next(0, possibleNextPlaces.Length)];

            return new DumbRobot(next, null);
        }
    }
}
