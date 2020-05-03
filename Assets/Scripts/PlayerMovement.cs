using UnityEngine;

namespace TwilightRun
{
    public class PlayerMovement : MonoBehaviour
    {
        private enum PlayerVerticalPosition
        {
            LightDownAndDarkUp,
            LightUpAndDarkDown
        }

        [SerializeField] private GameObject _playerLight;
        [SerializeField] private GameObject _playerDark;
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private float _verticalSpeedMultiplier;
        [SerializeField] private float OnGroundY;
        [SerializeField] private float OnCeilingY;

        private Vector2 _horizontalMovementPerFrameVector;
        private Vector2 _verticalMovementPerFrameVectorUp;
        private Vector2 _verticalMovementPerFrameVectorDown;
        private float _verticalSpeed;
        private float _verticalMovementPerFrame;
        private PlayerVerticalPosition _desiredVerticalPosition = PlayerVerticalPosition.LightDownAndDarkUp;
        private bool _isChangingVerticalPosition = false;

        private void Awake()
        {
            CalculateVelocities();
        }

        private void OnValidate()
        {
            CalculateVelocities();
        }

        private void FixedUpdate()
        {
            Move();
            SetRotation();
        }

        private void Move()
        {
            Vector2 playerLightMovement = Vector2.zero;
            Vector2 playerDarkMovement = Vector2.zero;

            playerDarkMovement += _horizontalMovementPerFrameVector;
            playerLightMovement += _horizontalMovementPerFrameVector;

            if (_isChangingVerticalPosition)
            {
                float playerDarkDesiredY = _desiredVerticalPosition == PlayerVerticalPosition.LightDownAndDarkUp ? OnCeilingY : OnGroundY;
                float playerLightDesiredY = _desiredVerticalPosition == PlayerVerticalPosition.LightDownAndDarkUp ? OnGroundY : OnCeilingY;
                if (Mathf.Abs(playerLightDesiredY - _playerLight.transform.position.y) < _verticalMovementPerFrame)
                {
                    //завершаем движение
                    float distance = Mathf.Abs(playerLightDesiredY - _playerLight.transform.position.y);
                    if (_playerLight.transform.position.y < playerLightDesiredY)
                    {
                        playerLightMovement.y += distance;
                        playerDarkMovement.y -= distance;
                    }
                    else
                    {
                        playerLightMovement.y -= distance;
                        playerDarkMovement.y += distance;
                    }
                    _isChangingVerticalPosition = false;
                }
                else
                {
                    if (_playerLight.transform.position.y < playerLightDesiredY)
                    {
                        playerLightMovement += _verticalMovementPerFrameVectorUp;
                        playerDarkMovement += _verticalMovementPerFrameVectorDown;
                    }
                    else
                    {
                        playerLightMovement += _verticalMovementPerFrameVectorDown;
                        playerDarkMovement += _verticalMovementPerFrameVectorUp;
                    }
                }
            }

            _playerLight.transform.Translate(playerLightMovement, Space.World);
            _playerDark.transform.Translate(playerDarkMovement, Space.World);
        }

        private void SetRotation()
        {
            if (!_isChangingVerticalPosition)
                return;
            float playerLightRotation;
            float playerDarkRotation;
            if (_desiredVerticalPosition == PlayerVerticalPosition.LightDownAndDarkUp)
            {
                playerLightRotation = Mathf.Lerp(0, 180, (_playerLight.transform.position.y - OnGroundY) / (OnCeilingY - OnGroundY));
                playerDarkRotation = Mathf.Lerp(0, 180, (_playerDark.transform.position.y - OnGroundY) / (OnCeilingY - OnGroundY));
            }
            else
            {
                playerLightRotation = Mathf.Lerp(0, 180, (_playerLight.transform.position.y - OnGroundY) / (OnCeilingY - OnGroundY));
                playerDarkRotation = Mathf.Lerp(0, 180, (_playerDark.transform.position.y - OnGroundY) / (OnCeilingY - OnGroundY));
            }
            _playerLight.transform.eulerAngles = new Vector3(playerLightRotation, 0, 0);
            _playerDark.transform.eulerAngles = new Vector3(playerDarkRotation, 0, 0);
        }

        private void CalculateVelocities()
        {
            _horizontalMovementPerFrameVector = new Vector2(_horizontalSpeed * Time.fixedDeltaTime, 0);
            _verticalSpeed = _horizontalSpeed * _verticalSpeedMultiplier;
            _verticalMovementPerFrame = _verticalSpeed * Time.fixedDeltaTime;
            _verticalMovementPerFrameVectorUp = new Vector2(0, _verticalMovementPerFrame);
            _verticalMovementPerFrameVectorDown = -_verticalMovementPerFrameVectorUp;
        }

        public void StartSwapping()
        {
            if (_isChangingVerticalPosition)
                return;
            _isChangingVerticalPosition = true;
            _desiredVerticalPosition = _desiredVerticalPosition == PlayerVerticalPosition.LightDownAndDarkUp ? 
                PlayerVerticalPosition.LightUpAndDarkDown : PlayerVerticalPosition.LightDownAndDarkUp;
        }
    } 
}
