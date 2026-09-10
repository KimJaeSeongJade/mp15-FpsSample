using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _magazine;
    private PlayerWeapon _weapon;

    private void Awake() => CacheComponents();
    private void Update() => RefreshMagazineUI();
    
    private void CacheComponents()
    {
        _weapon = GetComponentInChildren<PlayerWeapon>();
    }

    public void RefreshMagazineUI()
    {
        _magazine.text = $"{_weapon.CurrentMagazine} / {_weapon.MaxMagazine}";
    }
}
