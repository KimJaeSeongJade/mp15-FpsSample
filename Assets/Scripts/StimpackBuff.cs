using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StimpackBuff : MonoBehaviour
{
    private float _elapsedTime;
    
    private int _damage;
    private float _delay;
    private float _originMoveSpeed;
    private float _moveSpeed;
    private float _originFireCooldown;
    private float _fireCooldown;
    private PlayerController _controller;

    private void Update()
    {
        UpdateElapsedTime();
        Deactivate();
    }
    
    public StimpackBuff SetDamage(int damage)
    {
        _damage = damage;
        return this;
    }

    public StimpackBuff SetFireCooldown(float cooldown)
    {
        _originFireCooldown = _controller.Stat.WeaponCooldown;
        _fireCooldown = cooldown;
        return this;
    }

    public StimpackBuff SetMoveSpeed(float moveSpeed)
    {
        _originMoveSpeed = _controller.Stat.MoveSpeed;
        _moveSpeed = moveSpeed;
        return this;
    }

    public StimpackBuff SetDelay(float delay)
    {
        _delay = delay;
        return this;
    }

    public StimpackBuff SetPlayerController(PlayerController controller)
    {
        _controller = controller;
        return this;
    }

    public void Activate()
    {
        Debug.Log("스팀팩 효과 시작");
        
        transform.parent = _controller.transform;

        _controller.Stat.WeaponCooldown = _fireCooldown;
        _controller.Stat.MoveSpeed = _moveSpeed;
        _controller.TakeDamage(_damage);
    }

    private void Deactivate()
    {
        if (_elapsedTime < _delay) return;
        
        _controller.Stat.WeaponCooldown = _originFireCooldown;
        _controller.Stat.MoveSpeed = _originMoveSpeed;
        Debug.Log("스팀팩 효과 종료");
        Destroy(gameObject);
    }

    private void UpdateElapsedTime()
    {
        _elapsedTime += Time.deltaTime;
    }
}
