using System.Collections.Generic;

namespace EloquentRobot
{
    public record Robot(string Direction, Queue<string> Memory = null);

    public record RouteRobot(string Direction, Queue<string> Memory) : Robot(Direction, Memory);

    public record GoalOrientedRobot(string Direction, Queue<string> Memory) : Robot(Direction, Memory);

    public record LazyRobot(string Direction, Queue<string> Memory) : Robot(Direction, Memory);
}
