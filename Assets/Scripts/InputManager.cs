using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    [SerializeField] private string _moveX = "Horizontal";
    [SerializeField] private string _moveY = "Vertical";
    [SerializeField] private string _rotateX = "Mouse X";
    [SerializeField] private string _rotateY = "Mouse Y";
    
    [SerializeField] private KeyCode _fire = KeyCode.Mouse0;
    [SerializeField] private KeyCode _reload = KeyCode.R;
    [SerializeField] private KeyCode _interact = KeyCode.E;
    
    public event Action Fire;
    public event Action Interact;
    public event Action Reload;
    public event Action<Vector2> Move;
    public event Action<Vector2> Rotate;
    
    private static InputManager _instance;
    public static InputManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InputManager>();
                DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }

    private void Awake() => SetSingleton();

    private void Update() => GetInput();
    private void OnDestroy() => CleanUp();

    private void CleanUp()
    {
        _instance = null;
    }

    private void GetInput()
    {
        Rotate?.Invoke(GetRotate());
        Move?.Invoke(GetMove());
        if(Input.GetKey(_fire)) Fire?.Invoke();
        if(Input.GetKeyDown(_interact)) Interact?.Invoke();
        if(Input.GetKeyDown(_reload)) Reload?.Invoke();
    }
    
    private Vector3 GetRotate()
    {
        float x = Input.GetAxis(_rotateX);
        float y = Input.GetAxis(_rotateY);

        return new Vector3(-y, x, 0);
    }

    private Vector2 GetMove()
    {
        float x = Input.GetAxisRaw(_moveX);
        float y = Input.GetAxisRaw(_moveY);

        return new Vector2(x, y).normalized;
    }

    private void SetSingleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}