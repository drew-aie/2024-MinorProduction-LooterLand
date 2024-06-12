using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField, Tooltip("The pause menu.")]
    private GameObject _pauseMenuUI;

    private PlayerControls _playerActions;
    private InputAction _menu;

    public static bool _gameIsPaused = false;

    private void Awake()
    {
        //Creating Player Controller
        _playerActions = new PlayerControls();
    }

    private void OnEnable()
    {
        //Storing and enabling pause action
        _menu = _playerActions.Menu.Pause;
        _menu.Enable();

        _menu.performed += MenuCall;
    }

    private void OnDisable()
    {
        //Self explanatory
        _menu.Disable();
    }

    public void MenuCall(InputAction.CallbackContext context)
    {
        //Only running once while button is pressed
        if (!context.started)
            return;

        //Check if game is already paused
        if (_gameIsPaused)
            Resume();
        else
            Pause();
    }

    public void Pause()
    {
        //Activating pause menu
        _pauseMenuUI.SetActive(true);
        //Freezing the game world
        Time.timeScale = 0.0001f;

        _gameIsPaused = true;
    }

    //Unpauses game
    public void Resume()
    {
        //Deactivating pause menu
        _pauseMenuUI.SetActive(false);
        //Unfreezing game world
        Time.timeScale = 1.0f;

        _gameIsPaused = false;
    }

    //Reloads the current scene
    public void Retry()
    {
        //Loading current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Unfreezing game
        Time.timeScale = 1.0f;
    }

    public void OptionsMenu()
    {
        //SetActive options UI
    }

    //Loads the first scene in the index (Main Menu)
    public void QuitMenu()
    {
        //Restoring game time and loading main menu
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
