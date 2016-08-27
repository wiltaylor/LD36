using Zenject;

namespace Assets.Scripts.Input
{
    public enum ScrollDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public class ScrollSignal : Signal<ScrollDirection>
    {
        public class Trigger : TriggerBase
        {
            
        }
    }
}
