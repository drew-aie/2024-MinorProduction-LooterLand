using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIndicator : MonoBehaviour
{
    [SerializeField, Tooltip("The image that displays where the enemy is.")]
    private Image _enemyIndicator;

    [SerializeField, Tooltip("The image that displays where the enemy is offscreen.")]
    private Image _offScreenEnemyIndicator;

    [Space]

    [SerializeField, Tooltip("The main camera.")]
    private Camera _camera;

    [SerializeField, Tooltip("")]
    private GameObject _target;

    [Space]

    [SerializeField, Tooltip("")]
    private float _outOfSightOffset = 20f;

    private RectTransform _canvasTransform;

    private RectTransform _rectTransform;

    public float OutOfSightOffset
    {
        get => _outOfSightOffset;
    }

    // Start is called before the first frame update
    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
