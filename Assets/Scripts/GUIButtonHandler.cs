using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GUIButtonHandler : MonoBehaviour
{
    //public GameObject menu;
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject continueButton;
    //public GameObject playAgainButton;

    private bool sceneLoaded = false;
    private bool gamePaused = false;

    //private int curScore;
    //private int curLives;

    //public string[] levelSceneNames;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainMenu.SetActive(true);

        //Start with pause and gameover menu's disabled
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);

        sceneLoaded = false;
    }

    // Update is called once per frame
    void Update()
    {
        showPauseMenu();
    }

    public void loadGame()
    {
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        mainMenu.SetActive(false);

        Debug.Log("Hide all menus");

        //need a way to ensure that this menu does not get destroyed between scene loads
        DontDestroyOnLoad(gameObject);

        SceneManager.LoadScene("Level01");

        Time.timeScale = 1;
        sceneLoaded = true;
    }

    public void hideAllMenus()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    public void exitGame()
    {
        Application.Quit();
        Debug.Log("Exit Application..");
    }

    public void showGameOver()
    {
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);

        //Show the continue button only if the player won

        //playAgainButton.SetActive(true);
    }

    public void unpauseGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        gamePaused = false;
    }

    public void showPauseMenu()
    {
        //look and see if the user paused the game on controller or keyboard
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Joystick1Button9))
        {
            //if the scene is loaded, allows player to pause
            if (sceneLoaded)
            {
                if (!gamePaused)
                {
                    pauseMenu.SetActive(true);

                    //actually pause the game
                    Time.timeScale = 0;
                    gamePaused = true;
                }
                else
                {
                    unpauseGame();
                }
            }
        }
    }
}
