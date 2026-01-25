using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    [SerializeField] private Image _blackImage;
    [SerializeField] private float _imageAlpha;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn()
    {
        _imageAlpha = 1;

        while (_imageAlpha > 0)
        {
            _imageAlpha -= Time.fixedDeltaTime;
            _blackImage.color = new Color(0, 0, 0, _imageAlpha );
            yield return new WaitForSeconds(0);
        }
    }

    IEnumerator FadeOut()
    {
        _imageAlpha = 0;

        while (_imageAlpha < 0)
        {
            _imageAlpha += Time.fixedDeltaTime;
            _blackImage.color = new Color(0, 0, 0, _imageAlpha);
            yield return new WaitForSeconds(0);
        }
        
        SceneManager.LoadScene("02");
    }
}
