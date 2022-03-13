using ItemsLibrary.Actions;
using ItemsLibrary.Interfaces;

namespace ItemsLibrary
{
    public class WorldGoverment
    {
        public WorldGoverment(int timeSearchDistance)
        {
            TimeSearchDistance = timeSearchDistance;
        }

        public int TimeSearchDistance { get; }

        public List<ITransformAction<World>> SearchForward(World world)
        {
            for (long i = 1; i <= TimeSearchDistance; i++)
            {
                var availableActionsList = world.PredictActionsInFuture(i);
                if (availableActionsList.Any())
                    return availableActionsList.Select(action => (ITransformAction<World>) new CompositAction(new List<ITransformAction<World>>() { new TimePassAction(i), action })).ToList();
            }
            return new List<ITransformAction<World>>() { new TimePassAction(TimeSearchDistance) };
        }

        public static World Transform(World world, ITransformAction<World> action)
        {
            var newVersion = world.DeepClone();
            return action.Transform(newVersion);
        }
    }
}