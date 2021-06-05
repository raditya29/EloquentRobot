using System;

namespace EloquentRobot
{
    //public class Place : IEquatable<Place>
    //{
    //    private static int nextId = 1;

    //    public int Id { get; init; }
    //    public string Name { get; init; }

    //    public Place(string name)
    //    {
    //        Name = name ?? throw new ArgumentNullException(nameof(name));
    //        Id = nextId++;
    //    }

    //    public static bool operator ==(Place place1, Place place2) => place1.Equals(place2);

    //    public static bool operator !=(Place place1, Place place2) => !(place1 == place2);

    //    public bool Equals(Place other) => this.Id == other.Id;

    //    public override bool Equals(object obj) => Equals(obj as Place);

    //    public override int GetHashCode() => HashCode.Combine(Id.GetHashCode(), Name.GetHashCode());
    //}
}
