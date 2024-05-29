using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenuUI;

    private PlayerControls _playerActions;
    private InputAction _menu;

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
        if (_menu.WasPerformedThisFrame())
        {
            
            
        }
    }

    private void OnEnable()
    {
        //Storing and enabling pause action
        _menu = _playerActions.Menu.Pause;
        _menu.Enable();
    }

    private void OnDisable()
    {
        _menu.Disable();
    }

    public void MenuCall(InputAction.CallbackContext context)
    {
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
