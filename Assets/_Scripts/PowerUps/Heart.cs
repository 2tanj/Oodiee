using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public static Heart Instance;

    public bool HasBonusLife { get; set; }
    public bool HasBeenResurected { get; set; }

    void Awake() =>
        Instance = this;


}
