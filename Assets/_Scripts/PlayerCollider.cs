using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : IAudioPlayer
{
    [SerializeField]
    private AudioClip _deathSound;

    private bool _canDie = true;

    private void Awake()
    {
        SetupAudio();
    }

    private void Death()
    {
        PlaySound(_deathSound);
        Debug.LogError("GAME OVER!!");

        PlayerController.Instance._toSwim = false;
    }

    private IEnumerator DisableDeath()
    {
        _canDie = false;
        yield return new WaitForSeconds(2f);
        _canDie = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (ShieldPU.Instance.IsShielded)
        {
            ShieldPU.Instance.HitShield(collision.transform.position);
            return;
        }
        if (LifePU.Instance.HasBonusLife)
        {
            LifePU.Instance.DestroyHeart();
            StartCoroutine(DisableDeath());

            return;
        }
        
        if (!_canDie)
            return;

        Death();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IPowerUp>(out var powerUp))
        {
            powerUp.PickUp();
            return;
        }
    }
}
