namespace GC.Events
{
    public interface IBaseGameEventListener<in T>
    {
        void OnEventInvoked(T item);
    }
}