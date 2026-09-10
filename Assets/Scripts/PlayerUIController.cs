using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _magazine;
    
    public void RefreshMagazineUI(int magazine, int maxMagazine)
    {
        _magazine.text = $"{magazine} / {maxMagazine}";
    }
}
