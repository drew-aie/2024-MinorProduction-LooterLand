using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScrolling : MonoBehaviour
{
    [SerializeField, Tooltip("The Rect Transform the textmesh.")]
    private RectTransform _text;

    // Start is called before the first frame update
    void Start()
    {
        _text.DOMoveX(5.0f, 5.0f).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }
}
