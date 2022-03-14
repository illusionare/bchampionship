using ItemsLibrary.Interfaces;

namespace ItemsLibrary.Actors;

public class Counter : IUnit
{
    private long countedAmount;

    public Counter(int maximumAmount)
    {
        MaximumAmount = maximumAmount;
    }

    public long CountedAmount
    {
        get => countedAmount;
        set
        {
            countedAmount = value;
            countedAmount = countedAmount > MaximumAmount ? MaximumAmount : countedAmount;
        }
    }
    public long MaximumAmount { get; private set; }

    public IUnit DeepClone()
    {
        var clone = (Counter)MemberwiseClone();
        clone.CountedAmount = CountedAmount;
        clone.MaximumAmount = MaximumAmount;
        return clone;
    }

    public IUnit TimePass(long deltaTime)
    {
        return this;
    }
}
