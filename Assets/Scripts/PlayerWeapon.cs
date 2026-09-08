using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private Transform _cameraTransform;

    [SerializeField] private KeyCode _fireKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode _reloadKey = KeyCode.R;
    [SerializeField] private float _range;
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown;
    [SerializeField] private int _maxMagazine;
    [SerializeField] private FlameEffect _flameEffect;
    
    private float _currentCooldown;
    private int _currentMagazine;
    private bool _isPressedFire => Input.GetKey(_fireKey);
    private bool _isPressedReload => Input.GetKeyDown(_reloadKey);
    private bool _isReadyFire => _currentCooldown >= _cooldown;
    private bool _hasBullets => _currentMagazine > 0;
    private bool _canFire => _isPressedFire && _isReadyFire && _hasBullets;
    
    // ------------------------------------------
    private void Awake() => CacheComponents();
    private void Start() => Init();
    private void Update() => UpdateCooldown();
    // ------------------------------------------

    public void Fire()
    {
        if (!_canFire) return;
        
        _currentMagazine--;
        _currentCooldown = 0f;
        PlayFlameEffect();

        if (!TryGetDamageable(out IDamageable damageable)) return;
        
        damageable.TakeDamage(_damage);
    }

    private void PlayFlameEffect()
    {
        _flameEffect.gameObject.SetActive(true);
        _flameEffect.Play();
    }

    private bool TryGetDamageable(out IDamageable damageable)
    {
        bool result = false;
        damageable = null;
        
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, _range))
        {
            result = hit.transform.TryGetComponent(out damageable);
        }

        return result;
    }

    private void CacheComponents()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void Init()
    {
        _currentCooldown = 0f;
        _currentMagazine = _maxMagazine;
    }

    private void UpdateCooldown()
    {
        if (_isReadyFire) return;
        
        _currentCooldown += Time.deltaTime;
    }

    public void Reload()
    {
        if (!_isPressedReload) return;
        
        _currentMagazine = _maxMagazine;
    }
}
