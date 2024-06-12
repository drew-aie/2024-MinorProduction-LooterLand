using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndState : MonoBehaviour
{
    [SerializeField, Tooltip("The UI canvas for the ending screen.")]
    private GameObject _endScreenUI;

    [SerializeField, Tooltip("The UI prefab.")]
    private GameObject _ui;

    public void EndingCall()
    {
        //Enabling endscreenUI and disabling UI prefab
        _endScreenUI.SetActive(true);
        _ui.SetActive(false);

        //Freezing game world
        Time.timeScale = 0f;
    }

    //Reloads the current scene
    public void Retry()
    {
        //Loading current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Unfreezing game
        Time.timeScale = 1.0f;
    }

    //Loads the first scene in the index (Main Menu)
    public void QuitMenu()
    {
        //Restoring game time and loading main menu
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
