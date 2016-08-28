using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Actions
{
    public class ActionManager
    {

        [Inject] private UIAction[] _actions;

        public GameObject GetAction(string name) => _actions.FirstOrDefault(a => a.Name == name)?.Button;
    }
}
