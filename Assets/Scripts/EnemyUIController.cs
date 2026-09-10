using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour
{
    [SerializeField] private Image _gauge;
    [SerializeField] private TextMeshProUGUI _text;

    public void RefreshHealthUI(int health, int maxHealth)
    {
        _gauge.fillAmount = health / (float)maxHealth;
        _text.text = $"{health}/{maxHealth}";
    }
}
