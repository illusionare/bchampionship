using ItemsLibrary.Actions;
using ItemsLibrary.Actors;
using ItemsLibrary.Interfaces;

namespace ItemsLibrary.PotentialActions;

public class TransferGeneratedToCounterOnCap : IPotentialAction
{
    public bool PreCondition(World world, List<IUnit> units)
    {
        var counter = units.OfType<Generator>().FirstOrDefault();
        return counter?.GeneratedAmount == counter?.MaximumGeneratedAmount;
    }

    public World Transform(World world)
    {
        var copy = world.DeepClone();
        var generator = copy.Units.OfType<Generator>().FirstOrDefault();
        var counter = copy.Units.OfType<Counter>().FirstOrDefault();
        if (counter != null && generator != null)
        {
            counter.CountedAmount += generator.GeneratedAmount;
            generator.GeneratedAmount = 0;
        }

        return copy;
    }
}
