using UnityEngine;

namespace Game.PlayerComponents
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RotationLimiter : MonoBehaviour
    {
        [SerializeField] private float _maxAngle;

        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            var rotation = _rigidbody.rotation;
            _rigidbody.rotation = GetAdmissibleAngle(rotation, _maxAngle);
        }
        
        private float GetAdmissibleAngle(float angle, float maxAngle)
        {
            if (CheckAdmissibleAngle(angle, maxAngle)) return angle;

            return angle > maxAngle ? maxAngle : 360 - maxAngle;
        }
        
        private bool CheckAdmissibleAngle(float angle, float maxAngle)
        {
            return Mathf.Abs(angle) < maxAngle || Mathf.Abs(angle) > 360 - maxAngle;
        }
    }
}