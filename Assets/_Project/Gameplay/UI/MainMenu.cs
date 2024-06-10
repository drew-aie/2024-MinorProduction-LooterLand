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
    [SerializeField, Tooltip("How long the transition will last before main scene is loaded.")]
    private float _transitionTime = 3f;

    [SerializeField, Tooltip("The images that will be displayed in between loaded scenes.")]
    private Image[] _loadingImages;

    private Image _selectedImage;

    public void PlayGame()
    {
        StartCoroutine(Load(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1)));
        Transition();
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
    }

    private void Transition()
    {
        SelectRandomImage();
        _selectedImage.DOFade(1f, 2f);
    }

    private IEnumerator Load(Action callback)
    {
        yield return new WaitForSeconds(_transitionTime);

        callback();
    }
}
