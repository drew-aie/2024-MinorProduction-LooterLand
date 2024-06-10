using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIndicator : MonoBehaviour
{
    [SerializeField, Tooltip("The GameObject that will point towards enemies.")]
    private GameObject _indicator;

    [SerializeField, Tooltip("Who the indicator is for. (The Player)")]
    private NavMeshAgent _target;

    private Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_renderer.isVisible)
        {
            if (!_indicator.activeSelf)
                _indicator.SetActive(true);

            //Store direction to target
            Vector2 direction = _target.transform.position - transform.position;
            //Storing 2D physics raycast
            RaycastHit2D raycast = Physics2D.Raycast(transform.position, direction);

            //If raycast collider isn't null, indicator position is set to raycast point
            if (raycast.collider)
                _indicator.transform.position = raycast.point;
        }
        else
        {
            if (_indicator.activeSelf)
                _indicator.SetActive(false);
        }
    }
}
