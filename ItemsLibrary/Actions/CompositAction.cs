using ItemsLibrary.Interfaces;

namespace ItemsLibrary.Actions;

public class CompositAction : ITransformAction<World>
{
    public List<ITransformAction<World>> Actions { get; }

    public CompositAction(List<ITransformAction<World>> actions)
    {
        Actions = actions;
    }
    public World Transform(World world)
    {
        var returnVar = world;
        foreach (var action in Actions)
        {
            returnVar = action.Transform(returnVar);
        }
        return returnVar;
    }
}
