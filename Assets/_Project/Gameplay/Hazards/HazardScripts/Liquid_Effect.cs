using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Liquid_Effect : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystemLeft;

    [SerializeField]
    private ParticleSystem _particleSystemRight;

    private void Awake()
    {
        Stop();
    }

    public void Play(Material material)
    {
        _particleSystemLeft.gameObject.SetActive(true);
        _particleSystemRight.gameObject.SetActive(true);

        _particleSystemLeft.GetComponent<ParticleSystemRenderer>().material = material;
        _particleSystemRight.GetComponent<ParticleSystemRenderer>().material = material;

        _particleSystemLeft.Play();
        _particleSystemRight.Play();
    }

    public void Stop()
    {
        _particleSystemLeft.Stop();
        _particleSystemRight.Stop();

        _particleSystemLeft.gameObject.SetActive(false);
        _particleSystemRight.gameObject.SetActive(false);
    }
}
