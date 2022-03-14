using ItemsLibrary.Actions;
using ItemsLibrary.Actors;
using ItemsLibrary.Interfaces;

namespace ItemsLibrary.PotentialActions;

public class TerminateOnCounterCap : WorldTerminateAction, IPotentialAction
{
    public bool PreCondition(World world, List<IUnit> units)
    {
        var counter = units.OfType<Counter>().FirstOrDefault();
        return counter?.CountedAmount == counter?.MaximumAmount;
    }
}