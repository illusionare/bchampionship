namespace ItemsLibrary.Interfaces
{
    public interface IPotentialAction
    {
        IPrerequisiteCondition Condition { get; }
        ITransformAction<World> Action { get; }
    }
}