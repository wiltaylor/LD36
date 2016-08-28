using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.PlayerControls
{
    public class PlayerBuildModeDrawer : MonoBehaviour
    {
        [Inject]
        private PlayerSessionModifiers _sessionModifiers;

        [Inject]
        private Camera _camera;

        [Inject(Id = "BuildingBlock")] private Sprite _BlockSprite;

        private SpriteRenderer _renderer;

        private Texture2D _texture;

        public void Start()
        {
            _renderer = gameObject.AddComponent<SpriteRenderer>();
            _renderer.sprite = Sprite.Create(_texture, new Rect(0, 0, 0.32f, 0.32f), Vector2.zero);
        }

        void Update()
        {
            if (!_sessionModifiers.PlacingBuilding)
            {
                _renderer.enabled = false;
                return;
            }

            _renderer.enabled = true;
            var mouseworld = _camera.ScreenToWorldPoint(Input.mousePosition);
            //var tileposition = CordUtil.WorldToTile(mouseworld);
            //var tileworld = CordUtil.TileToWorld(tileposition.First, tileposition.Second);

            transform.position = mouseworld;

            ////Need to flip y axis as GUI 0,0 starts at top left where as everything else it starts bottom left.
            //var start = new Vector2(tileworld.x, Screen.height - tileworld.y);
            //var end = new Vector2(tileworld.x + 0.32f, Screen.height - tileworld.y + 0.32f);

            //var size = new Vector2(end.x - start.x, end.y - start.y);

            //GUI.skin.box.normal.background = _texture;
            //GUI.Box(new Rect(start, size), GUIContent.none);
        }
    }
}
