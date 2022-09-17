using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectFade : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> textes;
    [SerializeField] private List<SpriteRenderer> sprites;
    [SerializeField] private List<Image> images;

    public IEnumerator FadeOut(float fadeTime = 1)
    {
        float t = 0;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float alpha = 1 - t / fadeTime;
            foreach (TextMeshProUGUI text in textes)
            {
                Color col = text.color;
                col.a = alpha;
                text.color = col;
            }
            foreach (SpriteRenderer sprite in sprites)
            {
                Color col = sprite.material.color;
                col.a = alpha;
                sprite.material.color = col;
            }
            foreach (Image image in images)
            {
                Color col = image.color;
                col.a = alpha;
                image.color = col;
            }
            yield return null;
        }
    }

    public IEnumerator FadeIn(float fadeTime = 1)
    {
        float t = 0;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float alpha = t / fadeTime;
            foreach (TextMeshProUGUI text in textes)
            {
                Color col = text.color;
                col.a = alpha;
                text.color = col;
            }
            foreach (SpriteRenderer sprite in sprites)
            {
                Color col = sprite.material.color;
                col.a = alpha;
                sprite.material.color = col;
            }
            foreach (Image image in images)
            {
                Color col = image.color;
                col.a = alpha;
                image.color = col;
            }
            yield return null;
        }
    }

}
