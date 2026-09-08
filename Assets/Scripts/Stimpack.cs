using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class Stimpack : MonoBehaviour, IInteractable
{
    [SerializeField] private StimpackBuff _buffPrefab;
    [SerializeField] private float _delay;
    [SerializeField] private int _damage;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _cooldown;
    
    private Outline _outline;
    
    public GameObject GameObject { get => gameObject; }

    private void Awake() => CacheComponents();
    private void Start() => Init();
    
    public void Targeting()
    {
        _outline.enabled = true;
    }

    public void Untargeting()
    {
        _outline.enabled = false;
    }

    public void Interact(IInteractor interactor)
    {
        if (!(interactor is PlayerController)) return;
        
        
        Instantiate(_buffPrefab)
            .SetPlayerController((PlayerController)interactor)
            .SetDamage(_damage)
            .SetFireCooldown(_cooldown)
            .SetMoveSpeed(_moveSpeed)
            .SetDelay(_delay)
            .Activate();
        
        Destroy(gameObject);
    }

    private void Init()
    {
        _outline.enabled = false;
    }

    private void CacheComponents()
    {
        _outline = gameObject.GetComponent<Outline>();
    }
}
