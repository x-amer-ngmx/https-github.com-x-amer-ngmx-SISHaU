namespace SISHaU.Adapter
{
    public interface IBaseModelBehavior<out TS>
    {
        TS Push();
    }
}