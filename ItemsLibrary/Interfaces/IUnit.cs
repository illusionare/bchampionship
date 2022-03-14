namespace ItemsLibrary.Interfaces;

public interface IUnit : IDeepClonable<IUnit>
{
    IUnit TimePass(long deltaTime);
}
