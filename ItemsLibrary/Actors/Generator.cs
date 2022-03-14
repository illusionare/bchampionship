using ItemsLibrary.Interfaces;

namespace ItemsLibrary.Actors;

public class Generator : IUnit
{
    public long GeneratingPower { get; private set; }
    public long MaximumGeneratedAmount { get; private set; }
    public long GeneratedAmount { get; set; }

    public Generator(long generatingPower, long maximumGeneratedAmount)
    {
        GeneratingPower = generatingPower;
        MaximumGeneratedAmount = maximumGeneratedAmount;
    }

    public IUnit DeepClone()
    {
        var clone = (Generator)MemberwiseClone();
        clone.GeneratedAmount = GeneratedAmount;
        clone.GeneratingPower = GeneratingPower;
        clone.MaximumGeneratedAmount = MaximumGeneratedAmount;
        return clone;
    }

    public IUnit TimePass(long deltaTime)
    {
        GeneratedAmount += deltaTime * GeneratingPower;
        GeneratedAmount = GeneratedAmount > MaximumGeneratedAmount ? MaximumGeneratedAmount : GeneratedAmount;
        return this;
        
    }
}
