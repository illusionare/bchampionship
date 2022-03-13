// See https://aka.ms/new-console-template for more information

using ItemsLibrary;
using ItemsLibrary.Interfaces;

int initialWorldTime = 0;
var initialWorld = new World(initialWorldTime);

initialWorld.Units.Add(new Generator(1000));
initialWorld.PotentialActions.Add(new TerminateWhenMillion());


var timeSearchDistance = 100;
var goverment = new WorldGoverment(timeSearchDistance);

Stack<World> searchList = new Stack<World>();
searchList.Push(initialWorld);

do {
    var currentWorld = searchList.Pop();

    List<ITransformAction<World>> actions = goverment.SearchForward(currentWorld);
    foreach (var action in actions)
    {
        var newWorld = global::ItemsLibrary.WorldGoverment.Transform(currentWorld, action);
        currentWorld.AddDescendant(newWorld, action);
        if (!newWorld.IsTerminal)
            searchList.Push(newWorld);
    }
}
while (searchList.Count > 0);


Console.WriteLine("Hello, World!");
