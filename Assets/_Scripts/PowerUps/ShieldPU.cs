using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPU : IAudioPlayer, IPowerUp
{
    public static ShieldPU Instance;
    public bool IsShielded { get; private set; }

    [SerializeField]
    private Shield _shield;

    [SerializeField]
    private float _duration;

    [SerializeField]
    private AudioClip _sound;

    private MeshRenderer _mesh;
    private SphereCollider _collider;

    void Awake()
    {
        Instance = this;
        SetupAudio();
    }

    private void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        _collider = GetComponent<SphereCollider>();


        PlayerController.Instance.Shield._shieldOn = true;
    }

    public void PickUp()
    {
        IsShielded = true;

        PlaySound(_sound);
        PlayerController.Instance.Shield.OpenCloseShield();

        StartCoroutine(Finish());
    }

    public void HitShield(Vector3 pos)
    {
        PlayerController.Instance.Shield.HitShield(pos);
    }

    private IEnumerator Finish()
    {
        _mesh.enabled = false;
        _collider.enabled = false;
        
        yield return new WaitForSeconds(_duration);
        IsShielded = false;
        PlaySound(_sound);
        PlayerController.Instance.Shield.OpenCloseShield();

        Destroy(gameObject);
    }
}
