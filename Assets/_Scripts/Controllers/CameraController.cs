using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerController _player;

    [SerializeField]
    private float _distanceFromPlayer, _positionOffset = 7;


    private void Update()
    {
        var pos = transform.position;
        
        pos.x = _player.transform.position.x / _positionOffset;
        pos.z = _player.transform.position.z / _positionOffset;
        pos.y = (_player.transform.position.y + _distanceFromPlayer);

        transform.position = pos;
    }
}
