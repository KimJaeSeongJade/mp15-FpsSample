using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameEffect : MonoBehaviour
{
    [SerializeField] private float _deactivateDelay;
    private float _elapsedTime;

    // ------------------------------------------
    private void OnEnable() => ResetElapsedTime();
    private void Start() => gameObject.SetActive(false);
    private void Update()
    {
        UpdateElapsedTime();
        Deactivate();
    }
    // ------------------------------------------
    
    public void Play()
    {
        ResetElapsedTime();
    }

    private void ResetElapsedTime()
    {
        _elapsedTime = 0;
    }

    private void UpdateElapsedTime()
    {
        _elapsedTime += Time.deltaTime;
    }

    private void Deactivate()
    {
        if (_elapsedTime < _deactivateDelay) return;
        
        gameObject.SetActive(false);
    }
}
