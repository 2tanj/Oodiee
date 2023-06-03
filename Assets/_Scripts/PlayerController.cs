using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRb;

    [SerializeField]
    private float _speed = .5f;

    private void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //var newPos = _player.transform.position;
        //newPos.z += _speed;
        //_player.transform.position = newPos;
        var pos = transform.position;
        pos.z += _speed;
        _playerRb.AddForce(transform.up * _speed);

    }
}
