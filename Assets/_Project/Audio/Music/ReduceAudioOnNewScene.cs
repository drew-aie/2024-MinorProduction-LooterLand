using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReduceAudioOnNewScene : MonoBehaviour
{
    [SerializeField, Tooltip("The amount that sound will be magnified upon leaving the original scene.")]
    [Range(0, 1)]
    private float _volumeMagnifier;

    private bool _soundIsReduced;

    void Start()
    {
        _soundIsReduced = false;
    }
    private void FixedUpdate()
    {
        if (!_soundIsReduced && SceneManager.GetActiveScene().buildIndex != 0)
        {
            gameObject.GetComponent<AudioSource>().volume *= _volumeMagnifier;

            _soundIsReduced = true;
        }
    }
}
