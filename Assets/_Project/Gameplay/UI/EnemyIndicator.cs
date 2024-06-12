using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    //[SerializeField, Tooltip("The UI Canvas prefab.")]
    //private GameObject _uICanvas;

    [SerializeField, Tooltip("The image that displays where the enemy is offscreen.")]
    private GameObject _enemyIndicator;

    private List<GameObject> _indicators;

    private int _indicatorIndex;

    // Update is called once per frame
    void Update()
    {
        Show();
    }

    private void Show()
    {
        _indicatorIndex = 0;

        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in objects)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

            if (screenPos.z < 0)
                screenPos *= -1;

            Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0) / 2;

            screenPos -= screenCenter;

            float angle = Mathf.Atan2(screenPos.y, screenPos.x);
            angle -= 90 * Mathf.Deg2Rad;

            float cos = Mathf.Cos(angle);
            float sin = Mathf.Sin(angle);

            screenPos = screenCenter + new Vector3 (sin * 150, cos * 150, 0);

            //The m in y = mx + b
            float m = cos / sin;

            Vector3 screenBounds = screenCenter * 0.9f;

            //Checking up and down
            if (cos >  0)
                //Up
                screenPos = new Vector3 (screenBounds.y / m, screenBounds.y, 0);
            else
                //Down
                screenPos = new Vector3(-screenBounds.y / m, -screenBounds.y, 0);

            //Checking if out of bounds
            if (screenPos.x > screenBounds.x)
                //Out of bounds to the right
                screenPos = new Vector3 (screenBounds.x, screenBounds.x * m, 0);
            else if (screenPos.x < screenBounds.x)
                //Out of bounds to the left
                screenPos = new Vector3(-screenBounds.x, -screenBounds.x * m, 0);

            screenPos += screenCenter;

            GameObject arrow = GetIndicator();
            arrow.transform.localPosition = screenPos;
            arrow.transform.localRotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        }
        _indicators.Clear();
    }

    private GameObject GetIndicator()
    {
        GameObject stored;

        if (_indicatorIndex < _indicators.Count)
            stored = _indicators[_indicatorIndex].gameObject;
        else
        {
            stored = Instantiate(_enemyIndicator);
            stored.transform.parent = transform;
            _indicators.Add(stored);
        }

        _indicatorIndex++;
        return stored;
    }
}
