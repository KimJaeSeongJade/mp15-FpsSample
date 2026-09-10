using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUIController : UIController
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _returnTitleButton;
    
    private GameUIManager _manager;

    private void OnEnable() => BindButtonEvents();
    private void OnDisable() => UnbindButtonEvents();
    
    public override void Init(GameUIManager uiManager)
    {
        _manager = uiManager;
    }
    
    public override void OnActivate()
    {
        _gameManager.Pause();
    }
    
    private void Deactivate()
    {
        OnDeactivate();
        gameObject.SetActive(false);
    }
    
    public override void OnDeactivate()
    {
        _gameManager.Run();
    }

    private void BindButtonEvents()
    {
        _continueButton.onClick.AddListener(Deactivate);
        _returnTitleButton.onClick.AddListener(ReturnToTitle);
    }

    private void UnbindButtonEvents()
    {
        _continueButton.onClick.RemoveListener(Deactivate);
        _returnTitleButton.onClick.RemoveListener(ReturnToTitle);
    }

    private void ReturnToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
