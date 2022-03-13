namespace ItemsLibrary.Interfaces
{
    public interface IPrerequisiteCondition
    {
        bool Match(List<IUnit> units);
    }
}