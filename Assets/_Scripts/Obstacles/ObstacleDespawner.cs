using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDespawner : MonoBehaviour
{
    // BUG: check trello
    // fix: destroy all children
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
