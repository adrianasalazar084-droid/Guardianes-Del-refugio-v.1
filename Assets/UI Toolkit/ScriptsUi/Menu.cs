using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class ScriptMenu : MonoBehaviour
{
private Button playButton;
    private Button quitButton;

private void OnEnable()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        VisualElement root = uiDocument.rootVisualElement;
        playButton = root.Q<Button>("PlayButton");
        quitButton = root.Q<Button>("QuitButton");

        playButton.clicked += PlayGame;
        quitButton.clicked += QuitGame;
    }
    void PlayGame()
        {
            Debug.Log("Play button clicked!");
        SceneManager. LoadScene("GameScene"); 

    }

    void QuitGame()
        {
            Debug.Log("Quit button clicked!");
            
            Application.Quit();
    }

    private void OnDisable()
    {
        playButton.clicked -= PlayGame;
        quitButton.clicked -= QuitGame;
    }
}

