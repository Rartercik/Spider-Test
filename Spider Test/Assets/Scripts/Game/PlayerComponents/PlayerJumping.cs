using Game.Tools;
using UnityEngine;

namespace Game.PlayerComponents
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerJumping : MonoBehaviour
    {
        [SerializeField] private LayerMask _obstacle;
        [SerializeField] private Rigidbody2D _leftLeg;
        [SerializeField] private Rigidbody2D _rightLeg;
        [SerializeField] private float _jumpForceMultiplier;
        [SerializeField] private float _legForceMultiplier;
        
        private Rigidbody2D[] _childrenRigidbody;

        private void Start()
        {
            _childrenRigidbody = GetComponentsInChildren<Rigidbody2D>();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (LayerTool.EqualLayers(_obstacle, other.gameObject.layer))
            {
                foreach (var rigidbody in _childrenRigidbody)
                {
                    rigidbody.velocity = Vector2.zero;
                }
            }
        }

        public void Jump(Vector2 force, Leg[] legs)
        {
            foreach (var rigidbody in _childrenRigidbody)
            {
                rigidbody.AddForce(force * _jumpForceMultiplier);
            }

            var stretchingLeg = force.x >= 0 ? _rightLeg : _leftLeg;
            
            stretchingLeg.AddForce(force * _legForceMultiplier);
        }

        public bool CheckJumpAvailable(Leg[] legs)
        {
            foreach (var leg in legs)
            {
                if (leg.Connected)
                {
                    return true;
                }
            }

            return false;
        }
    }
}