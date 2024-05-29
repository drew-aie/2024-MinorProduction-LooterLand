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
        //Deactivating pause menu before game starts
        //_pauseMenuUI.SetActive(false);

        //Creating Player Controller
        _playerActions = new PlayerControls();
        //_playerActions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("Update call");

            //Check if game is already paused
            if (_gameIsPaused)
                Resume();
            else
                Pause();
        }
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
        _menu.Disable();
    }

    public void MenuCall(InputAction.CallbackContext context)
    {
        Debug.Log("MenuCall");
        //Only running once while button is pressed
        if (!context.started)
            return;

        Debug.Log("Paused");

        //Check if game is already paused
        if (_gameIsPaused)
            Resume();
        else
            Pause();
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
        //Reloads the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OptionsMenu()
    {
        //SetActive options UI
    }

    public void QuitMenu()
    {
        //Restoring game time and loading main menu
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
