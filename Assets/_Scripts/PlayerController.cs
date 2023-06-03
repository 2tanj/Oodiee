using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // maxRotAngle
    // uvecaj currAngle za amount
    // clamp -> rotate

    [SerializeField, Range(0,5)]
    private float _speed = .5f, _steerSpeed = .1f;

    [SerializeField]
    private float _maxRotAngleX = 45f, _maxRotAngleY = 55f, _rotAmount = 1f;

    private Vector3 _rotAngle;

    [SerializeField]
    private float tiltAngle = 45.0f, smooth = 5.0f;

    private void Update()
    {
        //Smoothly tilts a transform towards a target rotation.
        float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle * -1;
        float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle * -1;

        //Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

        //Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);

        var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Swim(input);
        //Rotate(input);
    }

    private void Swim(Vector2 input)
    {
        var pos = transform.position;

        pos.x += input.x * _steerSpeed;
        pos.z += input.y * _steerSpeed * -1; 
        
        pos.y += _speed;

        transform.position = pos;
    }

    // BURN THIS 
    // x = z / y = x, 
    // inverted
    private void Rotate(Vector2 input)
    {
        var rotation = UnityEditor.TransformUtils.GetInspectorRotation(transform);
        // 1 = levo
        float rotX = 0, rotY = 0;
        bool xChanged = false, yChanged = false;

        //Debug.Log("input: " + input);

        #region X Rotation
        // no input case
        if (input.x == 0)
        {
            if (transform.rotation.z > 0)
                rotX = _rotAmount * -1;
            else if (transform.rotation.z < 0)
                rotX = _rotAmount;

            xChanged = true;
        }

        // okrenut do kraja case
        if (transform.rotation.z >= _maxRotAngleX)
        {
            if (input.x < 0)
                rotX = 0;

            xChanged = true;
        }
        else if (transform.rotation.z <= _maxRotAngleX * -1)
        {
            if (input.x > 0)
                rotX = 0;

            xChanged = true;
        }

        // else case
        if (!xChanged)
        {
            rotX = _rotAmount * input.x * -1;
        }
        #endregion

        if (input.y == 0)
        {
            if (rotation.x > 90)
                rotY = _rotAmount * -1;
            else if (rotation.x < 90)
                rotY = _rotAmount;

            yChanged = true;
        }

        // okrenut do kraja case
        if (rotation.x >= 90 + _maxRotAngleY)
        {
            if (input.y < 0)
                rotY = 0;

            yChanged = true;
        }
        else if (rotation.x <= 90 - _maxRotAngleY)
        {
            if (input.y > 0)
                rotY = 0;

            Debug.Log("caotest");


            yChanged = true;
        }

        // else case
        if (!yChanged)
        {
            rotY = _rotAmount * input.y * -1;
        }


        Debug.Log("y:" + rotY + " input: " + input);
        var rot = new Vector3(rotY, 0, rotX);
        //var rot = new Vector3(_rotAmount * input.y * -1, 0, rotX);


        transform.Rotate(rot);

    }
}
