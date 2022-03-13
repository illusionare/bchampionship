using ItemsLibrary.Interfaces;

namespace ItemsLibrary.Actions
{
    public class WorldTerminateAction : ITransformAction<World>
    {
        public World Transform(World world)
        {
            var copy = world.DeepClone();
            copy.IsTerminal = true;
            return copy;
        }
    }
}