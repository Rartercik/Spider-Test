using UnityEngine;
using UnityEngine.Events;

namespace Game.PlayerComponents
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _head;
        [SerializeField] private Sprite _deadHead;
        [SerializeField] private UnityEvent _onDead;
        
        private HingeJoint2D[] _childrenJoints;

        private void Start()
        {
            _childrenJoints = GetComponentsInChildren<HingeJoint2D>();
        }

        public void Die()
        {
            foreach (var joint in _childrenJoints)
            {
                joint.enabled = false;
            }

            _head.sprite = _deadHead;
            _onDead?.Invoke();
        }
    }
}