using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IInteractor
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Transform _cameraPivot;
    [SerializeField] private float _detectionRange;
    
    private PlayerWeapon _weapon;
    private PlayerMovement _movement;
    private Transform _cameraTransform;
    
    private IInteractable _targetInteractable;

    private bool _hasDetectInteractable => _targetInteractable != null;

    public GameObject GameObject { get => gameObject; }
    public InputManager PlayerInput => InputManager.Instance;

    // -----------------------------------------------
    private void Awake() => CacheComponents();
    private void OnEnable() => BindInputAcions();
    private void Update() => DetectInteractable();
    private void LateUpdate()
    {
        SetWeaponTransform();
        SetCameraTransform();
    }
    private void OnDisable() => UnbindInputAcions();
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

    public void DetectInteractable()
    {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, _detectionRange))
        {
            if (_hasDetectInteractable)
            {
                _targetInteractable.Untargeting();
                _targetInteractable = null;
            }

            return;
        }

        if (_hasDetectInteractable)
        {
            if (hit.collider.gameObject == _targetInteractable.GameObject)
            {
                return; // 같은 Interactable을 계속 주시하고 있는 경우
            }
        }

        _targetInteractable?.Untargeting();
        _targetInteractable = hit.collider.GetComponent<IInteractable>();
        
        _targetInteractable?.Targeting();
    }

    public void TryInteract()
    {
        if (!_hasDetectInteractable) return;
        
        _targetInteractable.Interact(this);
        _targetInteractable = null;
    }
}
