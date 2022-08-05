using UnityEngine;
using Zenject;

namespace Game.PlayerComponents
{
    [RequireComponent(typeof(LineRenderer))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Vector2 _firstPointOffset;
        [SerializeField] private float _maxLength;
        
        [Inject] private Player _player;
        
        private readonly float _minLengthToThrow = 0.1f;
        
        private LineRenderer _lineRenderer;
        private Transform _transform;
        private Transform _playerTransform;
        private Vector3 _firstPoint;
        private bool _isPressed;

        private float LineLength => Vector3.Distance(_lineRenderer.GetPosition(1), _lineRenderer.GetPosition(0));

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _transform = transform;
            _playerTransform = _player.transform;
        }

        private void Update()
        {
            ClickScreen();
        }

        private void ClickScreen()
        {
            var mousePosition = Input.mousePosition;

            if (Input.GetMouseButtonDown(0))
            {
                StartThrowing(mousePosition);
            }
            else if (_isPressed)
            {
                if (_player.CanJump)
                {
                    ProcessJumping(mousePosition);
                }
                else
                {
                    EndJumping();
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                EndJumping();
            }
        }

        private void StartThrowing(Vector3 mousePosition)
        {
            _firstPoint = mousePosition;
            _isPressed = true;
        }

        private void ProcessJumping(Vector3 mousePosition)
        {
            var playerPosition = _playerTransform.position;

            var firstLinePoint = GetFirstLinePoint(playerPosition);
            _lineRenderer.SetPosition(0, firstLinePoint);

            var secondPoint = GetSecondPoint(playerPosition, mousePosition);
            var secondLinePoint = GetSecondLinePoint(_lineRenderer.GetPosition(0), secondPoint);
            _lineRenderer.SetPosition(1, secondLinePoint);
        }

        private void EndJumping()
        {
            var force = _lineRenderer.GetPosition(1) - _lineRenderer.GetPosition(0);
            if (LineLength >= _minLengthToThrow)
            {
                _player.Jump(force);
            }
            SetLinePoints(Vector3.zero);

            _isPressed = false;
        }

        private Vector3 GetFirstLinePoint(Vector3 playerPosition)
        {
            playerPosition.z = _transform.position.z;
            return playerPosition + (Vector3)_firstPointOffset;
        }

        private Vector3 GetSecondPoint(Vector3 playerPosition, Vector3 mousePosition)
        {
            playerPosition.z = _transform.position.z;
            var firstPoint = _camera.ScreenToWorldPoint(_firstPoint);
            firstPoint.z = _transform.position.z;
            var mouseWorldPosition = _camera.ScreenToWorldPoint(mousePosition);
            mouseWorldPosition.z = _transform.position.z;

            return (playerPosition + (Vector3)_firstPointOffset) + (firstPoint - mouseWorldPosition);
        }

        private Vector3 GetSecondLinePoint(Vector3 firstPoint, Vector3 secondPoint)
        {
            var directionToSecondPoint = (secondPoint - firstPoint).normalized;
            var distanceToSecondPoint = Vector2.Distance(firstPoint, secondPoint);
            return firstPoint + directionToSecondPoint * Mathf.Clamp(distanceToSecondPoint, -_maxLength, _maxLength);
        }

        private void SetLinePoints(Vector3 point)
        {
            _lineRenderer.SetPosition(0, point);
            _lineRenderer.SetPosition(1, point);
        }
    }
}
