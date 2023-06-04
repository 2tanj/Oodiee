using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (ShieldPU.Instance.IsShielded)
        {
            ShieldPU.Instance.HitShield(collision.transform.position);
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
