using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance;

    [SerializeField]
    private TextMeshProUGUI _scoreText;

    void Awake() => 
        Instance = this;

    private void Update()
    {
        int score = (int)GameManager.Instance.Score;
        _scoreText.text = score.ToString();
    }
}
