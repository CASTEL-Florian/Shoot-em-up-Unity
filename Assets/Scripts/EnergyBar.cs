using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;
    [SerializeField] private Color color2;
    private Color baseColor;

    private void Start()
    {
        baseColor = healthBarImage.color;
        baseColor.a = 1;
    }
    public void UpdateBar(float fillAmount, bool useColor2)
    {
        healthBarImage.fillAmount = Mathf.Clamp(fillAmount, 0, 1f);
        healthBarImage.color = useColor2 ? color2 : baseColor;
    }
}

