using ItemsLibrary.Actions;
using ItemsLibrary.Interfaces;

namespace ItemsLibrary
{
    public class WorldGoverment
    {
        public WorldGoverment(World initialWorld, int timeSearchDistance)
        {
            InitialWorld = initialWorld;
            TimeSearchDistance = timeSearchDistance;
        }

        public World InitialWorld { get; }
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

        public List<World> GetTerminals()
        {
            var terminals = new List<World>();
            Search(InitialWorld, condition: (World w) => w.IsTerminal, terminals);
            return terminals;
        }

        private static void Search(World node, Func<World, bool> condition, List<World> resultList)
        {
            if (node == null)
                return;

            if (condition(node))
            {
                resultList.Add(node);
                return;
            }

            foreach (var descendant in node.descendantLinks)
            {
                Search(descendant.Descendant, condition, resultList);
            }
        }

        public static World Transform(World world, ITransformAction<World> action)
        {
            var newVersion = world.DeepClone();
            return action.Transform(newVersion);
        }

        public void Run()
        {
            Stack<World> searchList = new Stack<World>();
            searchList.Push(InitialWorld);

            do
            {
                var currentWorld = searchList.Pop();

                List<ITransformAction<World>> actions = SearchForward(currentWorld);
                foreach (var action in actions)
                {
                    var newWorld = Transform(currentWorld, action);
                    currentWorld.AddDescendant(newWorld, action);
                    if (!newWorld.IsTerminal)
                        searchList.Push(newWorld);
                }
            }
            while (searchList.Count > 0);

        }
    }
}