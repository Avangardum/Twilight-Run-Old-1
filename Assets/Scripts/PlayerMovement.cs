using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject _playerLight;
    [SerializeField] private GameObject _playerDark;
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _verticalSpeedMultiplier;

    private Vector2 _horizontalMovementPerFrameVector;
    private Vector2 _verticalMovementPerFrameVectorUp;
    private Vector2 _verticalMovementPerFrameVectorDown;
    private float _verticalSpeed;

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
        Vector2 _playerLightMovement = Vector2.zero;
        Vector2 _playerDarkMovement = Vector2.zero;
        _playerDarkMovement += _horizontalMovementPerFrameVector;
        _playerLightMovement += _horizontalMovementPerFrameVector;
        _playerLight.transform.Translate(_playerLightMovement);
        _playerDark.transform.Translate(_playerDarkMovement);
    }

    private void CalculateVelocities()
    {
        _horizontalMovementPerFrameVector = new Vector2(_horizontalSpeed * Time.fixedDeltaTime, 0);
        _verticalSpeed = _horizontalSpeed * _verticalSpeedMultiplier;
        _verticalMovementPerFrameVectorUp = new Vector2(0, _verticalSpeed * Time.fixedDeltaTime);
        _verticalMovementPerFrameVectorDown = -_verticalMovementPerFrameVectorUp;
    }
}
