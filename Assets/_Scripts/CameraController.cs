using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerController _player;
    private Camera _cam;

    [SerializeField]
    private float _distanceFromPlayer;

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        var pos = _cam.transform.position;
        pos.z = (_player.transform.position.z + _distanceFromPlayer);
        _cam.transform.position = pos;


    }
}
