namespace ItemsLibrary.Interfaces;

public interface IPotentialAction: ITransformAction<World>
{
    bool PreCondition(World world, List<IUnit> units);
}
