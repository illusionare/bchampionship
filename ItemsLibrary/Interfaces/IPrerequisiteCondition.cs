namespace ItemsLibrary.Interfaces;

public delegate bool ActionPrerequisiteDelegate(List<IUnit> units);
public interface IPrerequisiteCondition
{
    bool Match(List<IUnit> units);
}
