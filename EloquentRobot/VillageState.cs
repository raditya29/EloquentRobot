using System;
using System.Collections.Generic;
using System.Linq;

namespace EloquentRobot
{
    public class VillageState
    {
        public string Place { get; }
        public Parcel[] Parcels { get; }

        public VillageState(string place, Parcel[] parcels)
        {
            Place = place ?? throw new ArgumentNullException(nameof(place));
            Parcels = parcels ?? throw new ArgumentNullException(nameof(parcels));
        }

        public VillageState Move(string destination) => 
            !Program.RoadGraph[Place].Contains(destination)
            ? this
            : new VillageState(destination,
                               Parcels.Select(p => p.Place != Place ? p : new Parcel(destination, p.Address))
                                      .Where(p => p.Place != p.Address)
                                      .ToArray());

        public static VillageState Random(int parcelCount = 5) {
            var parcels = new Stack<Parcel>();
            for (int index = 0; index < parcelCount; index++)
            {
                var address = Program.RandomPick(Program.RoadGraph.Keys.ToArray());
                string place;
                do
                {
                    place = Program.RandomPick(Program.RoadGraph.Keys.ToArray());
                } while (place == address);

                parcels.Push(new Parcel(place, address));
            }

            return new VillageState("Post Office", parcels.ToArray());
        }

    }

}
