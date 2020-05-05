using UnityEngine;

namespace TwilightRun
{
    public class CameraFollowing : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [Range(0, 1)][SerializeField] private float _playerXPositionOnCameraPercentage;

        private float _xOffset;

        private void Awake()
        {
            float _desiredPlayerScreenXPosition = Screen.width * _playerXPositionOnCameraPercentage;
            float _desiredPlayerWorldXPosition = Camera.main.ScreenToWorldPoint(new Vector3(_desiredPlayerScreenXPosition, 0, 0)).x;
            transform.Translate(_player.transform.position.x - _desiredPlayerWorldXPosition, 0, 0);
            _xOffset = Camera.main.transform.position.x - _player.position.x;
        }

        private void LateUpdate()
        {
            transform.position = new Vector3(_player.position.x + _xOffset, transform.position.y, transform.position.z);
        }
    }

}