namespace SISHaU.Adapter.FluentBilder.FInterface
{
    public interface IBaseModelPooler<out T>
    {
        T Pool();
    }
}