using ItemsLibrary.Actions;
using ItemsLibrary.Interfaces;

namespace ItemsLibrary
{
    public class World : IDeepClonable<World>
    {
        /// <summary>
        /// Is world in a fninal state
        /// </summary>
        public bool IsTerminal { get; internal set; }

        /// <summary>
        /// Initial world time point, typically 0
        /// </summary>
        public long InitialWorldTime { get; }

        /// <summary>
        /// Current time in the world
        /// </summary>
        public long CurrentTime 
        { 
            get => currentTime;
            set
            { 
                long deltaTime = value - currentTime;
                
                foreach (var unit in Units)
                    unit.TimePass(deltaTime);
                
                currentTime = value;
            }
        }

        public World(int initialWorldTime)
        {
            InitialWorldTime = initialWorldTime;
            IsTerminal = false;
        }

        class DescendantLink
        {
            public World Descendant { get; }

            public DescendantLink(World descendant, ITransformAction<World> action)
            {
                Descendant = descendant;
                Action = action;
            }

            public ITransformAction<World> Action { get; }
        }

        private List<DescendantLink> descendantLinks = new List<DescendantLink>();

        public void AddDescendant(World newWorld, ITransformAction<World> action)
        {
            descendantLinks.Add(new DescendantLink(newWorld, action));
        }

        public List<ITransformAction<World>> PredictActionsInFuture(long deltaTime)
        {
            var futureUnits = Units.
                Select(unit => unit.DeepClone().TimePass(deltaTime)).
                ToList();

            return PotentialActions.
                Where(pAction => pAction.Condition.Match(futureUnits)).
                Select(pAction => pAction.Action).
                ToList();

        }

        public List<IUnit> Units = new();
        public List<IPotentialAction> PotentialActions = new();
        private long currentTime;

        public World DeepClone()
        {
            var copy = (World)MemberwiseClone();
            descendantLinks = new List<DescendantLink>();
            copy.Units = Units.Select(unit => unit.DeepClone()).ToList();
            return copy;
        }
    }

    public class Generator : IUnit
    {
        public long GeneratingPower { get; private set; }

        public Generator(long generatingPower)
        {
            GeneratingPower = generatingPower;
        }

        public long GeneratedAmount { get; private set; }

        public IUnit DeepClone()
        {
            var clone = (Generator)MemberwiseClone();
            clone.GeneratedAmount = GeneratedAmount;
            clone.GeneratingPower = GeneratingPower;
            return clone;
        }

        public IUnit TimePass(long deltaTime)
        {
            GeneratedAmount += deltaTime * GeneratingPower;
            return this;
            
        }
    }

    public class GenerateTillMillion : IPrerequisiteCondition
    {
        public bool Match(List<IUnit> units)
        {
            var generator = units.Cast<Generator>().FirstOrDefault();
            return generator?.GeneratedAmount > 1000000;
        }
    }

    public class TerminateWhenMillion : IPotentialAction
    {
        public IPrerequisiteCondition Condition => new GenerateTillMillion();
        public ITransformAction<World> Action => new WorldTerminateAction();
    }
}