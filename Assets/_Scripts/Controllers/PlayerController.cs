using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField]
    private bool _toSwim = true;

    [Header("Movement")]
    [SerializeField, Range(0,30)]
    private float _speed = .5f, _steerSpeed = .1f;
    
    [Header("Rotation")]
    [SerializeField]
    private float _tiltAngle = 45.0f, _smoothing = 5.0f;

    void Awake() =>
        Instance = this;

    private void Update()
    {
        //Smoothly tilts a transform towards a target rotation.
        float tiltAroundZ = Input.GetAxis("Horizontal") * _tiltAngle * -1;
        float tiltAroundX = Input.GetAxis("Vertical") * _tiltAngle * -1;

        //Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

        //Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * _smoothing);

        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Swim(input);
    }

    private void Swim(Vector2 input)
    {
        if (!_toSwim)
            return; 

        var pos = transform.position;

        pos.x += input.x * _steerSpeed * Time.deltaTime;
        pos.z += input.y * _steerSpeed * -1 * Time.deltaTime; 
        
        pos.y += _speed * Time.deltaTime;

        transform.position = pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
    }
}
