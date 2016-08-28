using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zenject;

namespace Assets.Scripts.TileMap
{
    public class TileClickSignal : Signal<int, int, int>
    {
        public class Trigger : TriggerBase { }
    }
}
