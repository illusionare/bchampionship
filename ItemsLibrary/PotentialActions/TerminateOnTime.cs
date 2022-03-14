using ItemsLibrary.Actions;
using ItemsLibrary.Interfaces;

namespace ItemsLibrary.PotentialActions;

public class TerminateOnTime : WorldTerminateAction, IPotentialAction
{
    public TerminateOnTime(int terminationTime)
    {
        TerminationTime = terminationTime;
    }

    public int TerminationTime { get; }

    public bool PreCondition(World world, List<IUnit> units)
    {
        return world.CurrentTime > TerminationTime;
    }
}
