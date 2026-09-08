using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _mouseSensitivity;
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;

    private float _pitch;
    private Rigidbody _rigidbody;
    private PlayerStat _stat;
    
    private float _moveSpeed => _stat.MoveSpeed;

    private void Awake() => CacheComponents();

    public void Rotate()
    {
        Vector3 input = ReadRotateInput() * _mouseSensitivity;
        
        // 좌우 -> 회전
        transform.Rotate(0, input.y, 0, Space.Self);
        
        // 상하 -> 범위 내로 들어오게 해야됨.
        _pitch = Mathf.Clamp(_pitch + input.x, _minPitch, _maxPitch);
        //     -> Pivot을 회전시켜야 함.
        _cameraPivot.localRotation = Quaternion.Euler(_pitch, 0, 0);
    }
    
    private Vector3 ReadRotateInput()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        return new Vector3(-y, x, 0);
    }

    public void Move()
    {
        Vector3 input = ReadMoveInput();
        // 새로운 벨로시티 값 설정
        Vector3 direction =
            transform.right * input.x +
            transform.forward * input.z;
        
        Vector3 newVelocity = new Vector3(
            direction.x * _moveSpeed,
            _rigidbody.velocity.y,
            direction.z * _moveSpeed
        );
        
        // _rigidbody.velocity에 적용
        _rigidbody.velocity = newVelocity;
    }

    

    private Vector3 ReadMoveInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        return new Vector3(x, 0, z).normalized;
    }

    private void CacheComponents()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _stat = GetComponent<PlayerStat>();
    }
}
