using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndState : MonoBehaviour
{
    [SerializeField, Tooltip("The UI canvas for the ending screen.")]
    private GameObject _endScreenUI;

    private GameObject _ui;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EndingCall()
    {
        _endScreenUI.SetActive(true);
        Time.timeScale = 0f;
        //Do more stuff
    }

    //Reloads the current scene
    public void Retry()
    {
        //Loading current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //Unfreezing game
        Time.timeScale = 1.0f;
    }

    public void QuitMenu()
    {
        //Restoring game time and loading main menu
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }
}
