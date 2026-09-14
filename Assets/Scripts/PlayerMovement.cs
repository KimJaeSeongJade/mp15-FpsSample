using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;

    private float _pitch;
    private Rigidbody _rigidbody;

    private void Awake() => CacheComponents();

    public void Rotate(Vector2 input)
    {
        Vector3 dir = input * _mouseSensitivity;
        
        // 좌우 -> 회전
        transform.Rotate(0, dir.y, 0, Space.Self);
        
        // 상하 -> 범위 내로 들어오게 해야됨.
        _pitch = Mathf.Clamp(_pitch + dir.x, _minPitch, _maxPitch);
        //     -> Pivot을 회전시켜야 함.
        _cameraPivot.localRotation = Quaternion.Euler(_pitch, 0, 0);
    }

    public void Move(Vector2 input)
    {
        // 새로운 벨로시티 값 설정
        Vector3 direction =
            transform.right * input.x +
            transform.forward * input.y;
        
        Vector3 newVelocity = new Vector3(
            direction.x * _moveSpeed,
            _rigidbody.velocity.y,
            direction.z * _moveSpeed
        );
        
        // _rigidbody.velocity에 적용
        _rigidbody.velocity = newVelocity;
    }

    private void CacheComponents()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}
