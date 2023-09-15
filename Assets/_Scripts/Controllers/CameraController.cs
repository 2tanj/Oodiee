using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera _camera;
    CinemachineFramingTransposer _cameraBody;

    //[SerializeField]
    //private PlayerController _player;

    //[SerializeField]
    //private float _distanceFromPlayer, _positionOffset = 7;

    private void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        _cameraBody = _camera.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    private void Update()
    {
        _cameraBody.m_ScreenX = .5f;
        _cameraBody.m_ScreenY = .5f;

        _cameraBody.m_DeadZoneWidth = 0f;
        _cameraBody.m_DeadZoneHeight = 0f;

        //var pos = transform.position;
        
        //pos.x = _player.transform.position.x/* / _positionOffset*/;
        //pos.z = _player.transform.position.z / _positionOffset;
        //pos.y = (_player.transform.position.y + _distanceFromPlayer);

        //transform.position = pos;
    }
}
