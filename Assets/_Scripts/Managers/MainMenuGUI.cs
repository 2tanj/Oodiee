using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuGUI : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    [SerializeField]
    private Animator _animator;

    public void Animate()
    {
        _image.gameObject.SetActive(true);
        _animator.SetTrigger("StartTrigger");

        StartCoroutine(SwitchScenes());
    }

    private IEnumerator SwitchScenes()
    {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}