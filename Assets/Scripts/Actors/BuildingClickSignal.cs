using Zenject;

namespace Assets.Scripts.Actors
{
    public class BuildingClickSignal : Signal<int, BuildingController>
    {
        public class Trigger : TriggerBase
        {
            
        }
    }
}
