﻿using System;
using Assets.Scripts.TileMap;
using UnityEngine;
using Zenject;

namespace Assets
{
    public class TileBlockController : MonoBehaviour
    {
        public int LeftTileX;
        public int BottomTileY;

        [Inject(Id = "TileWidth")]
        private int TileWidth { get; set; }

        [Inject(Id = "TileHeight")]
        private int TileHeight { get; set; }

        [Inject] private Camera _camera;

        [Inject] public Sprite Sprite;

        [Inject] public TileSet _tileset;

        [Inject] private TileClickSignal.Trigger _trigger;

        [Inject]
        public void Construct()
        {
            var render = gameObject.AddComponent<SpriteRenderer>();
            render.sprite = Sprite;
            render.color = Color.white;
            var col = gameObject.AddComponent<BoxCollider2D>();

            col.size = new Vector2(Sprite.texture.width*0.01f, Sprite.texture.height*0.01f);
            col.offset = new Vector2(col.size.x/2, col.size.y/2);
            col.isTrigger = true;
        }

        public void SetTile(int x, int y, TileTypes type)
        {
            if (x < LeftTileX || x > LeftTileX + TileWidth - 1)
                return;
            if (y < BottomTileY || y > BottomTileY + TileHeight - 1)
                return;

            var realx = x - LeftTileX;
            var realy = y - BottomTileY;

            var spritedata = _tileset.GetTile(type);

            var tiledata = spritedata.texture.GetPixels(0, 0, TileWidth, TileHeight);
            for (var i = 0; i < tiledata.Length; i++)
                tiledata[i].a = 1f;

            //Debug.Log($"X: {realx} Y: {realy}");
            Sprite.texture.SetPixels(TileWidth * realx, TileHeight * realy, TileWidth, TileHeight, tiledata);
            //Debug.Log($"X: {realx} Y: {realy} - OK");

        }

        public void Apply()
        {
            Sprite.texture.Apply();
        }

        void OnMouseDown()
        {

            var worldcords = _camera.ScreenToWorldPoint(Input.mousePosition);
            var cords = worldcords - transform.position;

            var tilex = (int)((cords.x / 32) * 100) + LeftTileX;
            var tiley = (int)((cords.y / 32) * 100) + BottomTileY;

            _trigger.Fire(tilex, tiley);
        }

        public class Factory : Factory<TileBlockController>
        {
            
        }
    }
}