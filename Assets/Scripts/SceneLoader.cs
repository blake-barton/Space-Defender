using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// use SceneManagement for changing between scenes
public class SceneLoader : MonoBehaviour
{
    // config variables
    [SerializeField] float secondsToDelayDeathScene = 2f;
    [SerializeField] float secondsToDelayVictoryScene = 4f;

    // cached references
    GameSession gameSession;
    MusicPlayer musicPlayer;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        musicPlayer = FindObjectOfType<MusicPlayer>();
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadFirstScene()
    {
        // if statement for if the player is starting the game from the death screen.
        if (gameSession)
        {
            gameSession.ResetGame();
            musicPlayer.ChangeTrack((int)MusicPlayer.TrackEnumerator.mainMenu);
        }
        SceneManager.LoadScene(0);
    }

    public void LoadPreviousScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadSceneByName(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadGameScene()
    {
        // if statement for if the player is starting the game from the death screen.
        if (gameSession)
        {
            gameSession.ResetGame();
        }
        SceneManager.LoadScene("Game Screen");
        musicPlayer.ChangeTrack((int)MusicPlayer.TrackEnumerator.combat);
    }

    public void LoadGameOver()
    {
        StartCoroutine(DelayLoadGameOver());
    }

    public void LoadVictoryScene()
    {
        StartCoroutine(DelayLoadVictory());
    }

    IEnumerator DelayLoadGameOver()
    {
        yield return new WaitForSeconds(secondsToDelayDeathScene);
        LoadSceneByName("Death Screen");
        musicPlayer.PlayGameOverTrack();
    }

    IEnumerator DelayLoadVictory()
    {
        yield return new WaitForSeconds(secondsToDelayVictoryScene);
        LoadSceneByName("Victory Screen");
        musicPlayer.PlayVictoryTrack();
    }
}
