using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField, Tooltip("The canvas prefab that holds the main menu.")]
    private CanvasGroup _mainMenuCanvas;

    [SerializeField, Tooltip("How long the transition will last before main scene is loaded.")]
    private float _transitionTime = 3f;

    [Space]

    [SerializeField, Tooltip("The images that will be displayed in between loaded scenes.")]
    private Image[] _loadingImages;

    private Image _selectedImage;

    private Tween _fadeTween;

    public void PlayGame()
    {
        StartCoroutine(Load(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1)));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void SelectRandomImage()
    {
        //Storing a random int
        int rand = UnityEngine.Random.Range(0, 4);

        //Loading an image at the index of the random int
        _loadingImages[rand].enabled = true;
        //Storing image
        _selectedImage = _loadingImages[rand];
        Debug.Log("Image selected.");
    }

    private void FadeOutCanvas(float duration)
    {
        FadeCanvas(0f, duration, () => { _mainMenuCanvas.interactable = false; _mainMenuCanvas.blocksRaycasts = false; });
    }

    private void FadeCanvas(float endValue, float duration, TweenCallback onEnd)
    {
        if (_fadeTween != null)
            _fadeTween.Kill(false);

        _fadeTween = _mainMenuCanvas.DOFade(endValue, duration);
        _fadeTween.onComplete += onEnd;
    }

    private void FadeImage(float endValue, float duration)
    {
        if (_fadeTween != null)
            _fadeTween.Kill(false);

        SelectRandomImage();
        _fadeTween = _selectedImage.DOFade(endValue, duration);
    }

    private IEnumerator Load(Action callback)
    {
        yield return new WaitForSeconds(0.1f);
        FadeOutCanvas(1f);
        yield return new WaitForSeconds(3f);
        FadeImage(1f, 1f);
        //callback();
    }
}
