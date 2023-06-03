using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float Score { get => PlayerController.Instance.transform.position.y;
                         private set { } }

    // ubrzavanje vremenom
    // powerups
    // 

    private void Awake() => 
        Instance = this;


}
