using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenuUI;

    private PlayerControls _playerActions;

    public static bool _gameIsPaused = false;

    private void Awake()
    {
        //Grabbing Player Controller
        _playerActions = GetComponent<PlayerControls>();
        _playerActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        //If escape or start is pressed
        if (_playerActions.Pausing.Pause.ReadValue<bool>())
        {
            //Check if game is already paused
            if (_gameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    private void Pause()
    {
        //Activating pause menu
        _pauseMenuUI.SetActive(true);
        //Freezing the game world
        Time.timeScale = 0.0f;

        _gameIsPaused = true;
    }

    public void Resume()
    {
        //Deactivating pause menu
        _pauseMenuUI.SetActive(false);
        //Unfreezing game world
        Time.timeScale = 1.0f;

        _gameIsPaused = false;
    }

    public void Retry()
    {

    }

    public void OptionsMenu()
    {

    }

    public void QuitMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
