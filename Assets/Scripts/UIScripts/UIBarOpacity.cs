using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBarOpacity : MonoBehaviour
{
    public Image background;
    public Image sliderFill;

    void Start()
    {
        background.CrossFadeAlpha(0f, 0.01f, false);
        sliderFill.CrossFadeAlpha(0f, 0.01f, false);
    }

    void Update()
    {
        background.CrossFadeAlpha(0f, 0.25f, false);
        sliderFill.CrossFadeAlpha(0f, 0.25f, false);
    }

    public void OnBarValueChanged()
    {
        background.CrossFadeAlpha(255f, 0.01f, false);
        sliderFill.CrossFadeAlpha(255f, 0.01f, false);
    }
}
