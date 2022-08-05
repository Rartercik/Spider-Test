using Game.PlayerComponents;
using UnityEngine;
using Game.Tools;
using Zenject;

namespace Game.Environment
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DeathFloor : MonoBehaviour
    {
        [SerializeField] private LayerMask _playerPart;
        [SerializeField] private float _riseSpeed;
        
        [Inject] private Player _player;

        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var step = Vector2.up * (_riseSpeed * Time.deltaTime);
            var targetPosition = _rigidbody.position + step;
            _rigidbody.MovePosition(targetPosition);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (LayerTool.EqualLayers(other.gameObject.layer, _playerPart))
            {
                _player.Die();
                if (other.gameObject.TryGetComponent(out Rigidbody2D rigidbody))
                {
                    _rigidbody.velocity = rigidbody.velocity = Vector2.zero;
                    rigidbody.angularVelocity = 0;
                }
            }
        }
    }
}