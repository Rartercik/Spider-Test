using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Game.Tools;

namespace Game.PlayerComponents
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(FixedJoint2D))]
    public class Leg : MonoBehaviour
    {
        [SerializeField] private LayerMask _obstacle;
        [SerializeField] private Transform _head;
        [SerializeField] private UnityEvent _onConnected;
        [SerializeField] private UnityEvent _onDisconnected;

        [Inject] private Player _player;

        private Transform _transform;
        private FixedJoint2D _joint;
        private float _offsetFromHead;
        
        public bool Connected { get; private set; }

        private void Start()
        {
            _transform = transform;
            _joint = GetComponent<FixedJoint2D>();
            _offsetFromHead = GetOffsetFromHead(_transform.position, _head.position);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var offsetFromHead = GetOffsetFromHead(_transform.position, _head.position);
            if (LayerTool.EqualLayers(_obstacle, other.gameObject.layer) && (_player.CanJump == false || offsetFromHead >= _offsetFromHead))
            {
                Connect(other.collider);
            }
        }

        public void Disconnect()
        {
            _onDisconnected?.Invoke();
            _joint.enabled = false;
            Connected = false;
        }

        private void Connect(Collider2D connecting)
        {
            _onConnected?.Invoke();
            _joint.enabled = true;
                
            Connected = true;
            var legPoint = _transform.TransformPoint(_joint.anchor);
            var connectedPoint = connecting.bounds.ClosestPoint(legPoint);
            _joint.connectedAnchor = connectedPoint;
        }

        private float GetOffsetFromHead(Vector2 position, Vector2 headPosition)
        {
            return Math.Abs((position - headPosition).x);
        }
    }
}
