using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [Header("UIs")]
    [SerializeField] private UIController _menu;

    [SerializeField] private KeyCode _menuKey = KeyCode.Escape;
    
    
    private bool _isPressedMenu => Input.GetKeyDown(_menuKey);


    private void Update()
    {
        if (_isPressedMenu) Activate(_menu);
    }

    private void Activate(UIController ui)
    {
        ui.gameObject.SetActive(true);
        ui.OnActivate();
    }

    public void Deactivate(UIController ui)
    {
        ui.OnDeactivate();
        ui.gameObject.SetActive(false);
    }

    private void Init()
    {
        _menu.Init(this);
    }
}
