using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndState : MonoBehaviour
{
    [SerializeField, Tooltip("The UI canvas for the ending screen.")]
    private GameObject _endScreenUI;

    [SerializeField, Tooltip("The UI prefab.")]
    private GameObject _ui;

    [SerializeField, Tooltip("The player's collider.")]
    private CapsuleCollider _player;

    [SerializeField, Tooltip("The EndScoreUI within the End State prefab.")]
    private EndScoreUI _score;

    private bool _called;

    private void Awake()
    {
        _called = false;
    }

    public void EndingCall()
    {
        if (_called)
            return;

        _called = true;

        //Enabling endscreenUI and disabling UI prefab
        _endScreenUI.SetActive(true);
        _ui.SetActive(false);

        _score.ShowResults();
        _score.ScoreGrade();

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
