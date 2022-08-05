using UnityEngine;

namespace Game.PlayerComponents
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Transform _player;

        private Transform _transform;
        private Vector3 _offset;

        private void Start()
        {
            _transform = transform;
            _offset = _transform.position - _player.position;
        }

        private void Update()
        {
            var targetPosition = _transform.position;
            targetPosition.y = _player.position.y + _offset.y;
            _transform.position = targetPosition;
        }
    }
}