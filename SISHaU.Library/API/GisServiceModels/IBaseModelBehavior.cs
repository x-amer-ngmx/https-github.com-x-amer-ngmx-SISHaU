namespace SISHaU.Library.API.GisServiceModels
{
    public interface IBaseModelBehavior<out TS>
    {
        TS Push();
    }
}