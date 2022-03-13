namespace ItemsLibrary.Interfaces
{
    public interface ITransformAction<TSubject>
    {
        TSubject Transform(TSubject world);
    }
}