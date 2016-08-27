using Assets.Scripts.Input;
using Zenject;
using UnityEngine;

namespace Assets.Scripts.InputHandler
{
    public class InputController : ITickable
    {

        [Inject]
        private ScrollSignal.Trigger _scrollTrigger;

        public void Tick()
        {
            if (UnityEngine.Input.GetAxis("Vertical") > 0f)
            {
                _scrollTrigger.Fire(ScrollDirection.Up);
            }

            if (UnityEngine.Input.GetAxis("Vertical") < 0f)
            {
                _scrollTrigger.Fire(ScrollDirection.Down);
            }

            if (UnityEngine.Input.GetAxis("Horizontal") > 0f)
            {
                _scrollTrigger.Fire(ScrollDirection.Right);
            }

            if (UnityEngine.Input.GetAxis("Horizontal") < 0f)
            {
                _scrollTrigger.Fire(ScrollDirection.Left);
            }

            if (UnityEngine.Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                _scrollTrigger.Fire(ScrollDirection.ScrollDown);
            }

            if (UnityEngine.Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                _scrollTrigger.Fire(ScrollDirection.ScrollUp);
            }
        }
    }
}
