using System;
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

        [Inject(Optional = true)]
        public float ZoomSpeed = 10f;

        [Inject(Optional = true)]
        public float HighZoom = 5;

        [Inject(Optional = true)]
        public float LowZoom = 1;

        private float _minBoundX = 0f;

        private float _minBoundY = 0f;

        private float _maxBoundX = 42f;

        private float _maxBoundY = 42f;

        [Inject(Optional = true, Id = "MapWidth")]
        public int MapWidth = 128;

        [Inject(Optional = true, Id = "MapHeight")]
        public int MapHeight = 128;

        
        public void Initialize()
        {
            ScrollSignal.Event += OnScroll;
            _minBoundX = 0f;
            _minBoundY = 0f;
            _maxBoundX = MapWidth / 3.2f;
            _maxBoundY = MapHeight / 3.2f;
        }

        public void MoveToTile(int x, int y)
        {
            var loc = CordUtil.TileToWorld(x, y);

            Camera.transform.position = new Vector3(loc.x, loc.y, -10);
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
                case ScrollDirection.ScrollUp:
                    Camera.orthographicSize -= ZoomSpeed * Time.deltaTime;
                    break;
                case ScrollDirection.ScrollDown:
                    Camera.orthographicSize += ZoomSpeed * Time.deltaTime;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            pos = Camera.transform.position;

            if (Camera.orthographicSize < LowZoom)
                Camera.orthographicSize = LowZoom;

            if (Camera.orthographicSize > HighZoom)
                Camera.orthographicSize = HighZoom;

            if(Camera.transform.position.x > _maxBoundX)
                Camera.transform.position = new Vector3(_maxBoundX, pos.y, pos.z);

            if (Camera.transform.position.x < _minBoundX)
                Camera.transform.position = new Vector3(_minBoundX, pos.y, pos.z);

            if (Camera.transform.position.y > _maxBoundY)
                Camera.transform.position = new Vector3(pos.x, _maxBoundY, pos.z);

            if (Camera.transform.position.y < _minBoundY)
                Camera.transform.position = new Vector3(pos.x, _minBoundY, pos.z);

        }
    }
}
