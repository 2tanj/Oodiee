using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [field: SerializeField]
    public List<GameObject> PowerUps{ get; private set; }
}
