using Microsoft.VisualStudio.TestTools.UnitTesting;
using ItemsLibrary;
using ItemsLibrary.Actors;
using System.Collections.Generic;
using System.Linq;
using ItemsLibrary.PotentialActions;

namespace TestRunner;

[TestClass]
public class TestGoverment
{
    [TestInitialize]
    public void Init()
    {
    }

    [TestMethod]
    public void TerminateOnTime()
    {
        var initialWorld = new World(0);
        initialWorld.Units.Add(new Generator(1, 10));
        int terminationTime = 7;
        initialWorld.PotentialActions.Add(new TerminateOnTime(terminationTime));

        var goverment = new WorldGoverment(initialWorld, 5);
        goverment.Run();

        List<World> listOfTerminatedWorlds = goverment.GetTerminals();

        Assert.IsTrue(listOfTerminatedWorlds.First().CurrentTime >= terminationTime);

    }


    [TestMethod]
    public void CreateGeneratorGrowTo100AndExit()
    {
        var initialWorld = new World(0);
        initialWorld.Units.Add(new Generator(1, 10));
        initialWorld.PotentialActions.Add(new TerminateOnGeneratorCap());

        var goverment = new WorldGoverment(initialWorld, 5);
        goverment.Run();

        List<World> listOfTerminatedWorlds =  goverment.GetTerminals();

        Assert.AreEqual(listOfTerminatedWorlds.Count, 1);
        Assert.AreEqual(listOfTerminatedWorlds.First().Units.Cast<Generator>().First().GeneratedAmount , 10);
    }

    [TestMethod]
    public void CreateGeneratorAndExit()
    {
        int initialWorldTime = 0;
        var initialWorld = new World(initialWorldTime);

        initialWorld.Units.Add(new Generator(1, 10));
        initialWorld.Units.Add(new Counter(100));

        initialWorld.PotentialActions.Add(new TerminateOnCounterCap());
        initialWorld.PotentialActions.Add(new TransferGeneratedToCounterOnCap());


        var timeSearchDistance = 100;
        var goverment = new WorldGoverment(initialWorld, timeSearchDistance);
        goverment.Run();
    }
}


// TODO override toString methods in all classes
// TODO classes to records
// TODO termination rules. right now is is only 1 strict terminaton rule about time / need to be configurable e.g. if condition met, then no need to search futher

// TODO need the visualization of graph!!!!!