using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private bool _canDie = true;

    private IEnumerator DisableDeath()
    {
        _canDie = false;
        yield return new WaitForSeconds(3f);
        _canDie = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_canDie)
            return;

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

        Debug.LogError("GAME OVER!!");
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
