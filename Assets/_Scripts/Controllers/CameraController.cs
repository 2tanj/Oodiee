using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private bool _whichScene = true; // true = GameScene1 / false = GameScene
    CinemachineVirtualCamera _camera;
    CinemachineFramingTransposer _cameraBody;

    [SerializeField]
    private PlayerController _player;

    [SerializeField]
    private float _distanceFromPlayer, _positionOffset = 7;

    private void Start()
    {
        if (!_whichScene)
        {
            _camera = GetComponent<CinemachineVirtualCamera>();
            _cameraBody = _camera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
    }

    private void FixedUpdate()
    {
        if (!_whichScene)
        {
            //_cameraBody.m_ScreenX = .5f;
            //_cameraBody.m_ScreenY = .5f;

            //_cameraBody.m_DeadZoneWidth = 0f;
            //_cameraBody.m_DeadZoneHeight = 0f;
            return;
        }
        else
        {
            var pos = transform.position;

            // delete /positionOffset za ver2
            pos.x = _player.transform.position.x / _positionOffset * Time.deltaTime;
            pos.z = _player.transform.position.z / _positionOffset * Time.deltaTime;
            pos.y = (_player.transform.position.y + _distanceFromPlayer) /** Time.deltaTime*/;

            transform.position = pos;
        }
    }
}
