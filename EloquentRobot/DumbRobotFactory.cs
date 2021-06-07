namespace EloquentRobot
{
    public class DumbRobotFactory : IRobotFactory<DumbRobot>
    {
        public DumbRobot Create(string position, Parcel[] parcels) => new DumbRobot(position, null, parcels);
    }
}
