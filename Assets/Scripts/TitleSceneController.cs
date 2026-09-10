using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneController : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _quitButton;

    private void OnEnable() => BindButtonEvents();
    private void OnDisable() => UnbindButtonEvents();
    
    private void BindButtonEvents()
    {
        _startButton.onClick.AddListener(LoadGameScene);
        _quitButton.onClick.AddListener(Quit);
    }

    private void UnbindButtonEvents()
    {
        _startButton.onClick.RemoveListener(LoadGameScene);
        _quitButton.onClick.RemoveListener(Quit);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
