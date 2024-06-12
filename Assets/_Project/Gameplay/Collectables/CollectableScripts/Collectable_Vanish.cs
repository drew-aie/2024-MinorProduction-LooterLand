using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable_Vanish : MonoBehaviour
{
    [SerializeField, Tooltip("The item model.")]
    private GameObject _itemModel;

    //flips from true to false and vice versa
    //for setting the item's active status to simulate blinking.
    private bool _renderActive;

    [SerializeField, Tooltip("the time before the item disappears.")]
    private float _duration;

    [SerializeField, Tooltip("the amount of blinks the item does before it vanishes.")]
    private int _blinks;

    //begins the blinking coroutine.
    public void Start()
    {
        //true because the render is currently active.
        _renderActive = true;

        _duration /= 2;

        //begins the coroutine to blink the item out of existance after half the duration has passed.
        Invoke("StartBlinking", _duration);
    }

    //changes _renderActive to the opposite value.
    private void Flip()
    {
        _renderActive = !_renderActive;
    }

    private void StartBlinking()
    {
        //begins the coroutine to blink the item.
        StartCoroutine("Blink");
    }

    //Coroutine 
    private IEnumerator Blink()
    {
        //toggles the item's model on and off.
        for (int i = 0; i < (_blinks * 2); i++)
        {
            //if the item model exists
            if (_itemModel)
            {
                //flip the value of _renderActive
                Flip();

                //sets the itemmodel to the value of _renderActive.
                _itemModel.SetActive(_renderActive);
            }

            //waits for a fraction of the duration before this coroutine continues.
            yield return new WaitForSeconds(_duration / (_blinks * 2));
        }

        //despawns this gameobject from the world.
        Destroy(gameObject);
    }
}
