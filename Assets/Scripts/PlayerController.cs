using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerController : MonoBehaviour, IInteractor
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _detectionRange;
    [SerializeField] private float _tryInteactCooldown;

    private Coroutine _tryInteractRoutine;
    private WaitForSeconds _waitRetryInteract;
    private PlayerWeapon _weapon;
    private PlayerMovement _movement;
    private Transform _cameraTransform;
    
    private IInteractable _targetInteractable;

    private bool _hasDetectInteractable => _targetInteractable != null;

    public GameObject GameObject { get => gameObject; }
    public InputManager PlayerInput => InputManager.Instance;

    // -----------------------------------------------
    private void Awake()
    {
        CacheComponents();
        Init();
    }

    private void OnEnable()
    {
        BindInputAcions();
        StartDetectInteractable();
    }

    private void LateUpdate()
    {
        SetWeaponTransform();
        SetCameraTransform();
    }

    private void OnDisable()
    {
        UnbindInputAcions();
        StopDetectInteractable();
    }
    // -----------------------------------------------
    
    private void BindInputAcions()
    {
        PlayerInput.Move += _movement.Move;
        PlayerInput.Rotate += _movement.Rotate;
        PlayerInput.Fire += _weapon.Fire;
        PlayerInput.Reload += _weapon.Reload;
        PlayerInput.Interact += TryInteract;
    }

    private void UnbindInputAcions()
    {
        PlayerInput.Move -= _movement.Move;
        PlayerInput.Rotate -= _movement.Rotate;
        PlayerInput.Fire -= _weapon.Fire;
        PlayerInput.Reload -= _weapon.Reload;
        PlayerInput.Interact -= TryInteract;
    }

    private void CacheComponents()
    {
        _movement = GetComponent<PlayerMovement>();
        _weapon = GetComponentInChildren<PlayerWeapon>();
        _cameraTransform = Camera.main.transform;
    }

    private void SetWeaponTransform()
    {
        _weapon.transform.SetPositionAndRotation(
            _cameraPivot.position, 
            _cameraPivot.rotation
            );
    }
    
    private void SetCameraTransform()
    {
        _cameraTransform.SetPositionAndRotation(
            _cameraPivot.position,
            _cameraPivot.rotation
            );
    }

    private void StartDetectInteractable()
    {
        if (_tryInteractRoutine != null) return;

        _tryInteractRoutine = StartCoroutine(DetectInteractableRoutine());
    }

    private void StopDetectInteractable()
    {
        if (_tryInteractRoutine == null) return;
        
        StopCoroutine(_tryInteractRoutine);
        _tryInteractRoutine = null;
    }


    private IEnumerator DetectInteractableRoutine()
    {
        while (true)
        {
            yield return _waitRetryInteract;
            
            Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit, _detectionRange))
            {
                if (_hasDetectInteractable)
                {
                    _targetInteractable.Untargeting();
                    _targetInteractable = null;
                }

                continue;
            }

            if (_hasDetectInteractable)
            {
                if (hit.collider.gameObject == _targetInteractable.GameObject)
                {
                    continue; // 같은 Interactable을 계속 주시하고 있는 경우
                }
            }

            _targetInteractable?.Untargeting();
            _targetInteractable = hit.collider.GetComponent<IInteractable>();
        
            _targetInteractable?.Targeting();
        }
    }

    public void TryInteract()
    {
        if (!_hasDetectInteractable) return;
        
        _targetInteractable.Interact(this);
        _targetInteractable = null;
    }

    private void Init()
    {
        _waitRetryInteract = new WaitForSeconds(_tryInteactCooldown);
    }
}
