using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] float fadeOutTime;
    [SerializeField] float fadeInTime;
    private bool transitioning = false;
    public IEnumerator FadeOut(float time)
    {
        image.color = Color.clear;
        while (image.color.a < 1)
        {
            Color imageColor = image.color;
            imageColor.a += Time.unscaledDeltaTime / time;
            image.color = imageColor;
            yield return null;
        }
    }

    public IEnumerator FadeIn(float time)
    {
        image.color = Color.black;
        while (image.color.a > 0)
        {
            Color imageColor = image.color;
            imageColor.a -= Time.unscaledDeltaTime / time;
            image.color = imageColor;
            yield return null;
        }
    }

    public IEnumerator TransitionToScene(int sceneIndex)
    {
        if (!transitioning)
        {
            transitioning = true;
            yield return FadeOut(fadeOutTime);
            transitioning = false;
            Time.timeScale = 1;
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
