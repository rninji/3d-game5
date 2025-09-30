using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float distance = 3f;
    [SerializeField] private LayerMask obstacleLayerMask;

    private Transform _target;
    private Vector2 _lookVector;

    private float _azimuthAngle;
    private float _polarAngle;

    private void LateUpdate()
    {
        if (_target != null)
        {
            // 마우스 x, y 값 이용해 카메라 이동
            _azimuthAngle += _lookVector.x * rotationSpeed * Time.deltaTime;
            _polarAngle -= _lookVector.y * rotationSpeed * Time.deltaTime;
            _polarAngle = Mathf.Clamp(_polarAngle, -20f, 60f);
            
            // 벽 감지
            var currentDistance = AdjustCameraDistance();
            
            // 카메라 위치 설정
            var cartesianPosition = GetCameraPosition(distance, _polarAngle, _azimuthAngle);
            transform.position = _target.position + cartesianPosition;
            transform.LookAt(_target);
        }
    }

    public void SetTarget(Transform target, PlayerInput playerInput)
    {
        _target = target;
        
        // 카메라 초기 위치 설정
        var cartesianPosition = GetCameraPosition(distance, _polarAngle, _azimuthAngle);
        transform.position = _target.position + cartesianPosition;
        transform.LookAt(_target);

        // 마우스 이동
        playerInput.actions["Look"].performed += OnActionLook;
        playerInput.actions["Look"].canceled += OnActionLook;
    }

    void OnActionLook(InputAction.CallbackContext context)
    {
         _lookVector = context.ReadValue<Vector2>();
    }

    Vector3 GetCameraPosition(float r, float polarAngle, float azimusAngle)
    {
        float b = r * Mathf.Cos(polarAngle * Mathf.Deg2Rad);
        float x = b * Mathf.Sin(azimusAngle * Mathf.Deg2Rad);
        float y = r * Mathf.Sin(polarAngle * Mathf.Deg2Rad);
        float z = b * Mathf.Cos(azimusAngle * Mathf.Deg2Rad);

        return new Vector3(x, y, z);
    }

    float AdjustCameraDistance()
    {
        var currentDistance = distance;

        Vector3 direction = GetCameraPosition(1, _polarAngle, _azimuthAngle).normalized;
        RaycastHit hit;

        if (Physics.Raycast(_target.position, -direction, out hit, distance, obstacleLayerMask))
        {
            float offset = 0.3f;
            currentDistance = hit.distance - offset;
            currentDistance = Mathf.Max(currentDistance, 0.5f);
        }
        return currentDistance;
    }

}
