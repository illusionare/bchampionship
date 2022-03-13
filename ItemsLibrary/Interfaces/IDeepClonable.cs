namespace ItemsLibrary.Interfaces
{
    public interface IDeepClonable<T> where T : class
    {
        T DeepClone();
    }
}