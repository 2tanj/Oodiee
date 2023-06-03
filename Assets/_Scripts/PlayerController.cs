using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0,5)]
    private float _speed = .5f, _steerSpeed = .1f;

    private void Update()
    {
        Debug.Log(Input.GetAxisRaw("Horizontal"));

        Swim(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
    }


    private void Swim(Vector2 newPos)
    {
        var pos = transform.position;

        pos.x += newPos.x * _steerSpeed;
        pos.y += newPos.y * _steerSpeed;
        
        pos.z += _speed;

        transform.position = pos;
    }
}
