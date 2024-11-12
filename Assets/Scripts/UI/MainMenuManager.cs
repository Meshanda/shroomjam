using System;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _levelCanvas;
    [SerializeField] private GameObject _settingsCanvas;
    [SerializeField] private GameObject _creditsCanvas;

    private void Start()
    {
        ChangeCanvas(_mainMenuCanvas);
    }

    #region MainMenu
    
    public void ClickPlay()
    {
        ChangeCanvas(_levelCanvas);
    }

    public void ClickSetting()
    {
        ChangeCanvas(_settingsCanvas);
    }

    public void ClickCredits()
    {
        ChangeCanvas(_creditsCanvas);
    }

    #endregion

    #region LevelMenu
    
    public void ClickTutorial()
    {
        SceneLoader.LoadTutorial();
    }
    
    public void ClickLevel1()
    {
        SceneLoader.LoadLevel1();
    }

    #endregion
    
    public void ClickBack()
    {
        ChangeCanvas(_mainMenuCanvas);
    }
    
    private void ChangeCanvas(GameObject canvas)
    {
        _mainMenuCanvas.SetActive(false);
        _settingsCanvas.SetActive(false);
        _creditsCanvas.SetActive(false);
        _levelCanvas.SetActive(false);
        
        canvas.SetActive(true);
    }
}
