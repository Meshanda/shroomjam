using UnityEngine.SceneManagement;

public static class SceneLoader 
{
    private const string TUTORIAL_SCENE = "Tutorial";
    private const string LEVEL_ONE_SCENE = "Level1";
    private const string MAIN_MENU_SCENE = "MainMenu";
    
    public static void LoadMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE);
    }
    
    public static void LoadLevel1()
    {
        SceneManager.LoadScene(LEVEL_ONE_SCENE);
    }
    
    public static void LoadTutorial()
    {
        SceneManager.LoadScene(TUTORIAL_SCENE);
    }
    
    public static void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}