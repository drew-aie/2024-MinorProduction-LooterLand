using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField, Tooltip("The canvas prefab that holds the main menu.")]
    private CanvasGroup _mainMenuCanvas;

    [SerializeField, Tooltip("The parent holding the images and background.")]
    private GameObject _transitionImages;

    [SerializeField, Tooltip("How long the transition will last before main scene is loaded.")]
    private float _transitionTime = 3f;

    [Space]

    [SerializeField, Tooltip("The images that will be displayed in between loaded scenes.")]
    private Image[] _loadingImages;

    private Image _selectedImage;

    public void PlayGame()
    {
        //Starting a coroutine that loads the main scene after enough time has passed
        StartCoroutine(Load(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1)));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //Grabs a random image from the loadingImages array and fades it onto the screen
    private void SelectRandomImage()
    {
        //Storing a random int
        int rand = UnityEngine.Random.Range(0, 8);

        _transitionImages.SetActive(true);

        //Loading an image at the index of the random int
        _loadingImages[rand].enabled = true;
        //Storing image
        _selectedImage = _loadingImages[rand];

        //Setting images alpha to zero
        _selectedImage.DOFade(0f, 0f);
        //Fading image in
        _selectedImage.DOFade(1f, 3f);
    }

    //Grabs a random image before waiting and performing action callback
    private IEnumerator Load(Action callback)
    {
        SelectRandomImage();
        yield return new WaitForSecondsRealtime(_transitionTime);
        callback();
    }
}
