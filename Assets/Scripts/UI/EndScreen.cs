using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _subtitleText;
    
    public void Init(string title, string subtitle)
    {
        _titleText.text = title;
        _subtitleText.text = subtitle;
    }

    public void ClickReplay()
    {
        SceneLoader.ReloadScene();
    }
    
    public void ClickMainMenu()
    {
        SceneLoader.LoadMainMenu();
    }
}
