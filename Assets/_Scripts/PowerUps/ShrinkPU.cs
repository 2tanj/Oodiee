using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class ShrinkPU : IAudioPlayer, IPowerUp
{
    [SerializeField]
    private AudioClip _sound;

    [SerializeField]
    private float _duration, _reductionAmount, _numOfBoops = 3;

    [SerializeField]
    private List<MeshRenderer>   _mesh;
    private SphereCollider       _collider;

    private void Awake()
    {
        SetupAudio();

        _collider = GetComponent<SphereCollider>();
        foreach (var r in GetComponentsInChildren<MeshRenderer>())
        {
            _mesh.Add(r);
        }
    }

    public void PickUp()
    {
        PlaySound(_sound);

        PlayerController.Instance.transform.DOScale(PlayerController.Instance.transform.localScale / _reductionAmount, .2f);
        StartCoroutine(Finish());
    }

    private IEnumerator Finish()
    {
        _mesh.ForEach(m => m.gameObject.SetActive(false));
        _collider.enabled = false;
        yield return new WaitForSeconds(_duration);

        var sequence = DOTween.Sequence();
        var boopAmount = (PlayerController.Instance.transform.localScale * _reductionAmount) / 3.6f;

        for (int i = 0; i < _numOfBoops; i++)
        {
            sequence.Append(PlayerController.Instance.transform.DOPunchScale(
            punch: boopAmount,
            duration: .3f,
            vibrato: 5,
            elasticity: 1.2f));
        }

        //PlayerController.Instance.transform.DOScale(PlayerController.Instance.transform.localScale * _reductionAmount, .2f);
        sequence.OnComplete(() => PlayerController.Instance.transform.DOScale(PlayerController.Instance.transform.localScale * _reductionAmount, .2f));

        Destroy(gameObject);
    }
}
