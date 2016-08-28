using Assets.Scripts.PlayerControls;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Assets.Scripts.InputHandler
{
    public class InputController : ITickable
    {

        [Inject]
        private ScrollSignal.Trigger _scrollTrigger;

        [Inject]
        private Camera _camera;

        [Inject] private PlayerSessionModifiers _sessionModifiers;
        [Inject] private MouseReleaseSignal.Trigger _mouseReleaseTrigger;

        private bool _rightMouseDownHandled;

        public void Tick()
        {

            if(Input.GetKeyDown(KeyCode.LeftControl))
                _sessionModifiers.MultiSelectDown = true;
            if(Input.GetKeyUp(KeyCode.LeftControl))
                _sessionModifiers.MultiSelectDown = false;


            if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
            {
                var coords = _camera.ScreenToWorldPoint(Input.mousePosition);

                var hit = Physics2D.OverlapPoint(coords);

                if (hit != null)
                    hit.SendMessage("OnRightMouseDown");       
            }

            if(Input.GetMouseButtonUp(0))
                _mouseReleaseTrigger.Fire(0);

            if (Input.GetMouseButtonUp(1) && !EventSystem.current.IsPointerOverGameObject())
                _mouseReleaseTrigger.Fire(1);

            if (_sessionModifiers.Dragging)
                _sessionModifiers.DragCurrent = Input.mousePosition;
            
            if (Input.GetAxis("Vertical") > 0f)
            {
                _scrollTrigger.Fire(ScrollDirection.Up);
            }

            if (Input.GetAxis("Vertical") < 0f)
            {
                _scrollTrigger.Fire(ScrollDirection.Down);
            }

            if (Input.GetAxis("Horizontal") > 0f)
            {
                _scrollTrigger.Fire(ScrollDirection.Right);
            }

            if (Input.GetAxis("Horizontal") < 0f)
            {
                _scrollTrigger.Fire(ScrollDirection.Left);
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                _scrollTrigger.Fire(ScrollDirection.ScrollDown);
            }

            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                _scrollTrigger.Fire(ScrollDirection.ScrollUp);
            }
        }
    }
}
