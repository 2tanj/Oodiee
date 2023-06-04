using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathGUI : MonoBehaviour
{
    public static DeathGUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    private TextMeshProUGUI _text;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private Image _image;

    private bool _canSkip = false;

    private void Update()
    {
        if (PlayerController.Instance.IsDead && _canSkip && Input.anyKey)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Animate()
    {

        Debug.Log("cao");

        _image.gameObject.SetActive(true);
        //_animator.SetTrigger("AnimateTrigger");
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(.5f);
        _text.gameObject.SetActive(true);

        yield return new WaitForSeconds(.8f);
        _canSkip = true;
    }
}
