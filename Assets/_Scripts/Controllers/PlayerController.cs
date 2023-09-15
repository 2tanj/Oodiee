using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Camera _camera;

    [SerializeField]
    private InputAction _input;

    private Vector2 _startPoint;

    private bool _isDragging = false;

    [SerializeField]
    public bool _toSwim = true, _increaseSpeed = true;

    [Header("Movement")]
    [SerializeField, Range(0,30)]
    private float _speed = .5f, _steerSpeed = .1f;
    
    [Header("Rotation")]
    [SerializeField]
    private float _tiltAngle = 45.0f, _smoothing = 5.0f;

    [field: SerializeField]
    public GameObject HeartMesh { get; private set; }
    
    [field: SerializeField]
    public Shield Shield { get; private set; }

    public bool HasRevived { get; set; } = false;
    public bool IsDead { get; set; } = false;
    [field: SerializeField]
    public bool IsShielded { get; set; } = false;

    void Awake()
    {
        Instance = this;
        _camera  = Camera.main;
    }

    private void Update()
    {
        HandleMobileInput();
        var input = _isDragging ? (GetMobileInput() - _startPoint).normalized : Vector2.zero;

        _startPoint = GetMobileInput();

        //Debug.Log(_input.ReadValue<Vector2>());
        //Smoothly tilts a transform towards a target rotation.
        //float tiltAroundZ = Input.GetAxis("Horizontal") * _tiltAngle * -1;
        //float tiltAroundX = Input.GetAxis("Vertical") * _tiltAngle * -1;

        float tiltAroundZ = input.x * _tiltAngle * -1;
        float tiltAroundX = input.y * _tiltAngle * -1;

        //float tiltAroundZ = _input.ReadValue<Vector2>().x * _tiltAngle * -1;
        //float tiltAroundX = _input.ReadValue<Vector2>().y * _tiltAngle * -1;
        //Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

        //Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * _smoothing);

        //var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //Swim(_input.ReadValue<Vector2>());
        Swim(input);


        Debug.Log(input);
    }

    private void HandleMobileInput()
    {
        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            _isDragging = false;
            return;
        }

        if (!_isDragging)
        {
            _startPoint = GetMobileInput();
            _isDragging = true;
        }
    }

    private void Swim(Vector2 input)
    {
        if (!_toSwim)
            return; 

        var pos = transform.position;

        pos.x += input.x * _steerSpeed * Time.deltaTime;
        pos.z += input.y * _steerSpeed * -1 * Time.deltaTime; 
        
        if (_increaseSpeed)
            pos.y += _speed * (Time.timeSinceLevelLoad / 50) * Time.deltaTime;
        else
            pos.y += _speed * Time.deltaTime;

        transform.position = pos;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (ShieldPU.Instance.IsShielded)
    //    {
    //        ShieldPU.Instance.HitShield(collision.transform.position);
    //        return;
    //    }
    //    if (LifePU.Instance.HasBonusLife)
    //    {
    //        LifePU.Instance.DestroyHeart();
    //        return;
    //    }

    //    Debug.LogError("GAME OVER!!");
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.TryGetComponent<IPowerUp>(out var powerUp))
    //    {
    //        powerUp.PickUp();
    //        return;
    //    }
    //}

    private Vector2 GetMobileInput() =>
        Touchscreen.current.primaryTouch.position.ReadValue();

    private void OnEnable()  => _input.Enable();

    private void OnDisable() => _input.Disable();
}
