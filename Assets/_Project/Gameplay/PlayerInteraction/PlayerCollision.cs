using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The Manager that keeps track of increasing and decreasing of player score.")]
    private ScoreSystem _scoreSystem;

    [Space]

    [SerializeField, Tooltip("The speed dropped items will shoot out from the player.")]
    private float _itemDropSpeed;
    //stores true if the player can lose cash.
    private bool _canLoseCash;

    [SerializeField, Tooltip("The amount of time until the player can lose cash again.")]
    private float _protectionPeriodDuration;

    //property for _protectionPeriodDuration. [Read-Only]
    public float ProtectionPeriodDuration
    {
        get => _protectionPeriodDuration;
    }

    [Space]

    //event when the player collides with an enemy while having no cash.
    public UnityEvent OnHitWithoutCash;

    //stores a reference to the EffectHandler component.
    private EffectHandler _effectHandler;

    //stores a reference to the PlayerBlink component.
    private PlayerBlink _playerBlink;

    [SerializeField, Tooltip("The prefab of the dropped item")]
    private GameObject _dropPrefab;

    [SerializeField, Tooltip("The cash effect that is emmited on pickup of items.")]
    private ParticleSystem _cashEffect;    
    [SerializeField, Tooltip("The hit effect that is emmited on getting hit.")]
    private ParticleSystem _hitEffect;
    [SerializeField, Tooltip("The other hit effect that is emmited on getting hit.")]
    private ParticleSystem _otherHitEffect;


    private void Awake()
    {
        //true by default.
        _canLoseCash = true;

        //gets the EffectHandler component from the player.
        _effectHandler = GetComponent<EffectHandler>();

        //gets the FadePlayer component from the player.
        _playerBlink = GetComponent<PlayerBlink>();

        _cashEffect.Stop();

        _hitEffect.Stop();
        _otherHitEffect.Stop();
    }

    private void OnTriggerEnter(Collider collider)
    {
        //if the object that the player is colliding with is a collectable.
        if (collider.gameObject.TryGetComponent<Collectable>(out Collectable item))
        {
            //then
            //increase the current score by the cash value of item
            _scoreSystem.IncreaseScore(item.CashValue);

            if(item.CashValue > 45f && !item.CompareTag("Dropped"))
            {
                _cashEffect.Stop();
                _cashEffect.Play();
            }


            if(item.EffectID != 0)
            //shift the effect handler to the ID of this collectable.
            _effectHandler.Shift(item.EffectID);

            //remove the item in the scene
            Destroy(item.gameObject);
        }

        //if the object that the player is colliding with is an enemy
        if (collider.gameObject.CompareTag("Enemy"))
            HurtPlayer();

    }


    private void OnCollisionStay(Collision collision)
    {

        //if the object that the player is colliding with is still an enemy
        if (collision.gameObject.CompareTag("Enemy"))
            HurtPlayer();
    }

    private void HurtPlayer()
    {
        //if player cant lose cash
        if (!_canLoseCash)
            return;

        //emit hit particle effect.
        _hitEffect.Stop();
        _otherHitEffect.Stop();
        _hitEffect.Play();
        _otherHitEffect.Play();

        //begin a period where the Player cannot lose cash again for a set time.
        ProtectionPeriod();

        //get the score before its reduced
        float currentScore = _scoreSystem.CurrentScore;

        //then
        //reduce the current score by the reduce score value.
        _scoreSystem.ReduceScore();



        //if the player has no score
        if (currentScore < 1)
        {
            //call OnHitWithoutCash and return early.
            OnHitWithoutCash.Invoke();
            return;
        }

        //else

        //find the amount lost from the score.
        currentScore -= _scoreSystem.CurrentScore;

        _scoreSystem.CurrentScore -= 50;

        //divide the score by 3.
        currentScore *= 0.3f;


        //
        //spawn 3 dropped items in 3 directions.
        //

        //get a vector from forward
        Vector3 left = transform.forward.normalized;

        //find the perpendicular vector pointing horizontal from forward
        left = new Vector3(left.z, 0, -left.x);

        //get a vector in the opposite direction from left.
        Vector3 right = new Vector3(-left.x, 0, -left.z);

        //get the forward vector
        Vector3 back = transform.forward.normalized;

        //reverse the forward vector.
        back = new Vector3(-back.x, 0, -back.z);

        //create an array to store instantiated dropped items.
        GameObject[] drops = new GameObject[3];

        //instantiate prefabs of the dropped items
        for (int i = 0; i < drops.Length; i++)
        {
            drops[i] = Instantiate(_dropPrefab, transform.position, transform.rotation);

            //gets the Collectable component of the asset we instantiated.
            Collectable component = drops[i].GetComponent<Collectable>();

            //give the item value. (1/3 of what was reduced from the score.)
            component.CashValue = currentScore;

            //disables the collider of the object for a second to avoid picking back up.
            component.DelayPickup();
        }

        //add directional force to the dropped items to spread them out.
        drops[0].GetComponent<Rigidbody>().AddForce(left * _itemDropSpeed);
        drops[1].GetComponent<Rigidbody>().AddForce(right * _itemDropSpeed);
        drops[2].GetComponent<Rigidbody>().AddForce(back * _itemDropSpeed);

        //activates fading the player at the protection duration.
        _playerBlink.Activate(_protectionPeriodDuration);
    }


    //disables being able to lose cash for a set time.
    private void ProtectionPeriod()
    {
        _canLoseCash = false;

        Invoke("DisableProtection", _protectionPeriodDuration);
    }

    //enables the player being able to lose cash.
    private void DisableProtection()
    {
        _canLoseCash = true;
    }
}
