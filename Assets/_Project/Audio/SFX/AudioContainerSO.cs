using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioContainer", menuName = "ScriptableObject/AudioContainerSO")]
public class AudioContainerSO : ScriptableObject
{
    [SerializeField]
    private List<AudioClip> _clips;

    public List<AudioClip> Clips
    {
        get => _clips;  
    }
}
