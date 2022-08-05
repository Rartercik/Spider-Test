using UnityEngine;

namespace Game.PlayerComponents
{
    [RequireComponent(typeof(PlayerInteraction))]
    [RequireComponent(typeof(PlayerJumping))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Leg[] _legs;
        
        private PlayerInteraction _interaction;
        private PlayerJumping _jumping;
        
        public bool CanJump => _jumping.CheckJumpAvailable(_legs);

        private void Start()
        {
            _interaction = GetComponent<PlayerInteraction>();
            _jumping = GetComponent<PlayerJumping>();
        }

        public void Die()
        {
            DestroyLegJoints();
            _interaction.Die();
        }

        public void Jump(Vector2 force)
        {
            foreach (var leg in _legs)
            {
                leg.Disconnect();
            }

            _jumping.Jump(force, _legs);
        }

        private void DestroyLegJoints()
        {
            foreach (var leg in _legs)
            {
                leg.Disconnect();
                Destroy(leg);
            }
        }
    }
}