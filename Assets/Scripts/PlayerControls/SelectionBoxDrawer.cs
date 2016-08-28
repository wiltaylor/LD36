using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.PlayerControls
{
    public class SelectionBoxDrawer : MonoBehaviour
    {
        [Inject]
        private PlayerSessionModifiers _sessionModifiers;

        [Inject] private Camera _camera;

        private Texture2D _texture;

        public void Start()
        {
            var col = Color.blue;
            col.a = 0.4f;
            _texture = new Texture2D(1, 1);
            _texture.SetPixel(0,0, col);
            _texture.Apply();
        }
        
        void OnGUI()
        {
            if (!_sessionModifiers.Dragging)
                return;
            
            //Need to flip y axis as GUI 0,0 starts at top left where as everything else it starts bottom left.
            var start = new Vector2(_sessionModifiers.DragStart.x, Screen.height - _sessionModifiers.DragStart.y);
            var end = new Vector2(_sessionModifiers.DragCurrent.x,  Screen.height - _sessionModifiers.DragCurrent.y);
            
            var size = new Vector2(end.x - start.x, end.y - start.y);

            GUI.skin.box.normal.background = _texture;
            GUI.Box(new Rect(start, size), GUIContent.none);
        }
    }
}
