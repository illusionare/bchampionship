using ItemsLibrary.Interfaces;

namespace ItemsLibrary.Actions;

public class TimePassAction : ITransformAction<World>
{
    public long TimePass { get; }

    public TimePassAction(long timePass)
    {
        TimePass = timePass;
    }

    public World Transform(World world)
    {
        var newVersion = world.DeepClone();
        newVersion.CurrentTime += TimePass;
        return newVersion;
    }
}
