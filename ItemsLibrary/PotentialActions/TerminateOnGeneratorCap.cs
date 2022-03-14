using ItemsLibrary.Actions;
using ItemsLibrary.Actors;
using ItemsLibrary.Interfaces;

namespace ItemsLibrary
{
    public class TerminateOnGeneratorCap : WorldTerminateAction,  IPotentialAction
    {
        public bool PreCondition(World world, List<IUnit> units)
        {
            var counter = units.OfType<Generator>().FirstOrDefault();
            return counter?.GeneratedAmount == counter?.MaximumGeneratedAmount;
        }
    }
}