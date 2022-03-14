using ItemsLibrary.Interfaces;

namespace ItemsLibrary
{
    public class World : IDeepClonable<World>
    {
        /// <summary>
        /// Is world in a final state
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

        public class DescendantLink
        {
            public World Descendant { get; }

            public DescendantLink(World descendant, ITransformAction<World> action)
            {
                Descendant = descendant;
                Action = action;
            }

            public ITransformAction<World> Action { get; }
        }

        public List<DescendantLink> descendantLinks = new List<DescendantLink>();

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
                Where(pAction => pAction.PreCondition(this, futureUnits)).
                Select(pAction => (ITransformAction<World>)pAction).
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
}