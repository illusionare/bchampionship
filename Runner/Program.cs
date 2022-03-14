// See https://aka.ms/new-console-template for more information

using ItemsLibrary;
using ItemsLibrary.Actors;

int initialWorldTime = 0;
var initialWorld = new World(initialWorldTime);

initialWorld.Units.Add(new Generator(1, 10));
initialWorld.Units.Add(new Counter(100));

initialWorld.PotentialActions.Add(new TerminateONCounterCap());
initialWorld.PotentialActions.Add(new TransferGeneratedToCounterOnCap());


var timeSearchDistance = 100;
var goverment = new WorldGoverment(initialWorld, timeSearchDistance);
goverment.Run();


Console.WriteLine("Hello, World!");
