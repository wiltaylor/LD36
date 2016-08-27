using System;
using Assets.Scripts.Input;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.InputHandler
{
    public class CameraMover : IInitializable
    {
        [Inject]
        public Camera Camera;

        [Inject]
        public ScrollSignal ScrollSignal;

        [Inject(Optional = true)]
        public float ScrollSpeed = 10f;

        public void Initialize()
        {
            ScrollSignal.Event += OnScroll;
        }

        private void OnScroll(ScrollDirection direction)
        {
            var pos = Camera.transform.position;

            switch (direction)
            {
                case ScrollDirection.Up:
                    Camera.transform.position = new Vector3(pos.x, pos.y + ScrollSpeed * Time.deltaTime, pos.z);
                    break;
                case ScrollDirection.Down:
                    Camera.transform.position = new Vector3(pos.x, pos.y - ScrollSpeed * Time.deltaTime, pos.z);
                    break;
                case ScrollDirection.Left:
                    Camera.transform.position = new Vector3(pos.x - ScrollSpeed * Time.deltaTime, pos.y, pos.z);
                    break;
                case ScrollDirection.Right:
                    Camera.transform.position = new Vector3(pos.x + ScrollSpeed * Time.deltaTime, pos.y, pos.z);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }
}
