using Assets.Scripts.Actors;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class AttackSight : MonoBehaviour
    {

        private UnitController _controller;

        void OnTriggerEnter2D(Collider2D other)
        {
            if(_controller == null)
                _controller = transform.parent.GetComponent<UnitController>();

            if (other.tag != "Unit")
                return;

            if (_controller.TargeType != TragetType.None)
                return;

            var unit = other.GetComponent<UnitController>();

            if(unit.PlayerOwner != _controller.PlayerOwner)
                _controller.Attack(unit);
        }
    }
}
