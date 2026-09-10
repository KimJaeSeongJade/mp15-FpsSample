using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    [SerializeField] private Transform _gaugeTransform;
    [SerializeField] private Image _gauge;
    [SerializeField] private TextMeshProUGUI _text;

    private Transform _cameraTransform;
    
    private void Awake() => CacheComponents();
    private void LateUpdate() => SetRotation();
    
    public void RefreshHealthUI(int health, int maxHealth)
    {
        _gauge.fillAmount = health / (float)maxHealth;
        _text.text = $"{health}/{maxHealth}";
    }

    private void CacheComponents()
    {
        _cameraTransform = Camera.main.transform;
    }

    private void SetRotation()
    {
        _gaugeTransform.forward = _cameraTransform.forward;
    }
}
