using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private int _damage;
    private float _speed;

    // 어딘가에 부딪히면
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // TODO: 데미지 주기...
            Debug.Log("플레이어 맞음");
        }
        
        Destroy(gameObject);
    }

    private void Update() => MoveForward();
    
    // 앞으로 전진
    private void MoveForward()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
    }

    // 터렛으로부터 데이터 전달 받기.
    public void SetData(int damage, float speed,float destroyDelay)
    {
        _damage = damage;
        _speed = speed;
        
        // 스폰 기준으로 제한시간 이후 파괴
        Destroy(gameObject, destroyDelay);
    }
}
